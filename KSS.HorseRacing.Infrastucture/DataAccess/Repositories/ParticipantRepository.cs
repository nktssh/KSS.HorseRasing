﻿namespace KSS.HorseRacing.Infrastucture.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using KSS.HorseRacing.Infrastucture.DataAccess.Filters;
    using KSS.HorseRacing.Infrastucture.DataAccess.Interfaces;
    using KSS.HorseRacing.Infrastucture.DataModels;
    using KSS.HorseRacing.Infrastucture.Extensions;

    public class ParticipantRepository : BaseRepository, IParticipantRepository
    {
        public void Save(Participant participant)
        {
            save(participant);
        }

        public void Delete(Participant participant)
        {
            delete(participant);
        }

        public List<Participant> LoadParticipants(ParticipantFilter filter)
        {
            var queryable = getContext().Participants
                .WhereIf(filter.Id.HasValue, x => x.Id == filter.Id)
                .WhereIf(filter.RaceId.HasValue, x => x.Race.Id == filter.RaceId);

            if (filter.WithRacerHorceAndJockey)
            {
                queryable = queryable.Include(x => x.Racer).Include(x => x.Racer.Jockey).Include(x => x.Racer.Horse);
            }
            else
            {
                if (filter.WithRacer)
                {
                    queryable = queryable.Include(x => x.Racer);
                }

                if (filter.WithJockey)
                {
                    queryable = queryable.Include(x => x.Racer.Jockey);
                }

                if (filter.WithHorse)
                {
                    queryable = queryable.Include(x => x.Racer.Horse);
                }
            }

            var list = queryable.ToList();
            return list;
        }

        public int GetCountWinnerRaces(int participantId)
        {
            var count = getContext().Participants
                .Count(x => x.Id == participantId && (x.PlaceInRace == 1 || x.PlaceInRace == 2 || x.PlaceInRace == 3));
            return count;
        }

        public List<Participant> GetWinnersForYear(int year)
        {
            var participants = getContext().Participants
                .Where(x => x.Race.DateTimeOfRace.Year == year
                    && (x.PlaceInRace == 1 || x.PlaceInRace == 2 || x.PlaceInRace == 3));
            return participants.ToList();
        }
    }
}