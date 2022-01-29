using AutoMapper;
using System;
using Notes.Aplication.Interfaces;
using Notes.Aplication.Common.Mappings;
using Notes.Persistence;
using Xunit;

namespace Notes.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public NotesDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = NotesContextFactory.Create();
            var configurationProvaider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(INotesDbContext).Assembly));
            });
            Mapper = configurationProvaider.CreateMapper();
        }

        public void Dispose()
        {
            NotesContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
