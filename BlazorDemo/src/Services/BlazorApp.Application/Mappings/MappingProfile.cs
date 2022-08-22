using AutoMapper;
using BlazorApp.Application.Features.WorkItems.Commands.CreateWorkItem;
using BlazorApp.Application.Features.WorkItems.Commands.UpdateWorkItem;
using BlazorApp.Application.Features.WorkItems.Queries.GetWorkItemList;
using BlazorApp.Domain.Entities;
using System.Reflection;

namespace BlazorApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkItem, WorkItemViewModel>().ReverseMap();
            CreateMap<WorkItem, CreateWorkItemCommand>().ReverseMap();
            CreateMap<WorkItem, UpdateWorkItemCommand>().ReverseMap();
        }
    }
}