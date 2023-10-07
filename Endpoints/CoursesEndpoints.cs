using Carter;
using Microsoft.AspNetCore.Mvc;
using MongoDB_Test.Models;
using MongoDB_Test.Services;

namespace MongoDB_Test.Endpoints;

public class CoursesEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var courseGroup = app.MapGroup("course");
        
        courseGroup.MapGet("/", async (CourseService courseService) =>
        {
            return Results.Ok(await courseService.GetAllAsync());
        });

        courseGroup.MapGet("/{id}", async ([FromRoute] string id, CourseService courseService) => 
        {
            var course = await courseService.GetByIdAsync(id);

            if (course == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(course);
        });

        courseGroup.MapPost("/save", async ([FromBody] Course course, CourseService courseService) =>
        {
            await courseService.CreateAsync(course);
            return Results.Ok(course);
        });

        courseGroup.MapPut("/update/{id}", async ([FromRoute] string id, [FromBody] Course course, CourseService courseService) =>
        {
            await courseService.UpdateAsync(id, course);
            return Results.Ok();
        });

        courseGroup.MapDelete("/delete{id}", async ([FromRoute] string id, CourseService courseService) =>
        {
            await courseService.DeleteAsync(id);
            return Results.Ok();
        });
    }
}
