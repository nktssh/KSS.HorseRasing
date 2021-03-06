﻿using System.Collections.Generic;
using KSS.HorseRacing.Infrastucture.DataAccess.Filters;
using KSS.HorseRacing.Infrastucture.DataModels;

namespace KSS.HorseRacing.Infrastucture.DataAccess.Interfaces
{
    public interface IParticipantRepository : IBaseRepository
    {
        List<Participant> LoadParticipants(ParticipantFilter filter);

        int GetCountWinnerRaces(int participantId);

        List<Participant> GetWinnersForYear(int year);

        void Save(Participant participant);

        void Delete(Participant participant);
    }
}