using AutoMapper;
using Clinics.Core.DTOs;
using Clinics.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // src -> distnation
            CreateMap<Posts, postReadDto>();
            CreateMap<postCreateDto, Posts>();
            CreateMap<postReadDto, Posts>();
            CreateMap<Recipe, RecipeReadDto>();
            CreateMap<RecipeReadDto, Recipe>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<Clinic, ClinicDTO>();

            CreateMap<ClinicDTO, Clinic>();

            CreateMap<SymptomDTO, Symptom>();
            CreateMap<Symptom, SymptomDTO>();

            CreateMap<DiagnosisDTO, Diagnosis>();
            CreateMap<Diagnosis, DiagnosisDTO>();

            CreateMap<PostDoctorScheduleDTO, DoctorSchedule>();
            CreateMap<DoctorSchedule, PostDoctorScheduleDTO>();

            CreateMap<MedicalRecord, PostMedicalRecordDTO>();
            CreateMap<PostMedicalRecordDTO,MedicalRecord>();


        }

    }
}
