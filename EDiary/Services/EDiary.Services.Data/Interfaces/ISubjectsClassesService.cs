﻿namespace EDiary.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;

    public interface ISubjectsClassesService
    {
        bool Exist(Class @class, TypeOfClass typeOfClass, int subjectId, int schoolId);

        Task Create(Class @class, TypeOfClass typeOfClass, int subjectId, int schoolId);

        SubjectClass GetById(int id);

        Task Remove(int id);
    }
}