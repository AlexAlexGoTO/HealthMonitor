﻿using AutoMapper;
using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Common.Mappings;
using MedixineMonitor.Application.Observations.Commands;
using MedixineMonitor.Domain.Entities;
using NUnit.Framework;
using System.Runtime.Serialization;


namespace MedixineMonitor.UnitTests.Mapping
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(ObservationDto), typeof(Observation))]
        [TestCase(typeof(CreateOrUpdateObservationCommand), typeof(Observation))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
