using Carter;
using Microsoft.AspNetCore.Mvc;
using MongoDB_Test.Models;
using MongoDB_Test.Services;

namespace MongoDB_Test.Endpoints;

public class StudentEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var studentGroup = app.MapGroup("student");
        
        studentGroup.MapGet("/student", async ([FromServices] StudentService studentService) =>
        {
            return Results.Ok(await studentService.GetAllAsync());
        });

        studentGroup.MapGet("/student/{id}", async ([FromRoute] string id, [FromServices] StudentService studentService) => 
        {
            var student = await studentService.GetByIdAsync(id);

            if (student == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(student);
        });

        studentGroup.MapPost("/student/save", async ([FromBody] Student student, [FromServices] StudentService studentService) => 
        {
            await studentService.CreateAsync(student);

            return Results.Ok(student);
        });

        studentGroup.MapPut("/student/update/{id}", async ([FromRoute] string id, [FromBody] Student student, [FromServices] StudentService studentService) => 
        {
            await studentService.UpdateAsync(id, student);

            return Results.Ok();
        });

        studentGroup.MapDelete("/student/delete/{id}", async ([FromRoute] string id, [FromServices] StudentService studentService) => 
        {
            await studentService.DeleteAsync(id);

            return Results.Ok();
        });
    }
}
