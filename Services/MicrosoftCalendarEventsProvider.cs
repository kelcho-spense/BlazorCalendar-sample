using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Graph;
using Newtonsoft.Json;
using BlazorSample.Models;

namespace BlazorSample.Services
{
    public class MicrosoftCalendarEventsProvider : ICalendarEventsProvider
    {
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly GraphServiceClient _graphClient;

        public MicrosoftCalendarEventsProvider(IAccessTokenProvider accessTokenProvider, HttpClient httpClient)
        {
            _accessTokenProvider = accessTokenProvider;

            var graphAuthHandler = new MsalAuthenticationProvider(accessTokenProvider);
            _graphClient = new GraphServiceClient(graphAuthHandler);
        }

        public async Task<IEnumerable<CalendarEvent>> GetEventsInMonthAsync(int year, int month)
        {
            var startDateTime = new DateTime(year, month, 1);
            var endDateTime = startDateTime.AddMonths(1).AddDays(-1);

            try
            {
                var events = await _graphClient.Me.CalendarView
                    .Request(new List<QueryOption>()
                    {
                        new QueryOption("startdatetime", startDateTime.ToString("o")),
                        new QueryOption("enddatetime", endDateTime.ToString("o"))
                    })
                    .Select(e => new
                    {
                        e.Subject,
                        e.Start,
                        e.End
                    })
                    .GetAsync();

                var calendarEvents = new List<CalendarEvent>();

                foreach (var ev in events.CurrentPage)
                {
                    calendarEvents.Add(new CalendarEvent
                    {
                        Subject = ev.Subject,
                        StartDate = ev.Start.DateTime,
                        EndDate = ev.End.DateTime
                    });
                }

                return calendarEvents;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting events: {ex.Message}");
                return null;
            }
        }

        public async Task AddEventAsync(CalendarEvent calendarEvent)
        {
            var newEvent = new Event
            {
                Subject = calendarEvent.Subject,
                Start = new DateTimeTimeZone
                {
                    DateTime = calendarEvent.StartDate.ToString("o"),
                    TimeZone = TimeZoneInfo.Local.Id
                },
                End = new DateTimeTimeZone
                {
                    DateTime = calendarEvent.EndDate.ToString("o"),
                    TimeZone = TimeZoneInfo.Local.Id
                }
            };

            try
            {
                await _graphClient.Me.Events.Request().AddAsync(newEvent);
                Console.WriteLine("Event has been added successfully!");
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error adding event: {ex.Message}");
            }
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var tokenRequest = await _accessTokenProvider.RequestAccessToken(new AccessTokenRequestOptions
            {
                Scopes = new[] { "https://graph.microsoft.com/Calendars.ReadWrite" }
            });

            // Try to fetch the token 
            if (tokenRequest.TryGetToken(out var token))
            {
                if (token != null)
                {
                    return token.Value;
                }
            }

            return null;
        }
    }
}