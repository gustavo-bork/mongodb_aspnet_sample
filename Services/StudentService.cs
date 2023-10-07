﻿using MongoDB.Driver;
using MongoDB_Test.Models;

namespace MongoDB_Test.Services;

public class StudentService
{
    private readonly IMongoCollection<Student> _students;

    public StudentService(ISchoolDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _students = database.GetCollection<Student>(settings.StudentsCollectionName);
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _students.Find(_ => true).ToListAsync();
    }

    public async Task<Student> GetByIdAsync(string id)
    {
        return await _students.Find(student => student.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Student> CreateAsync(Student student)
    {
        await _students.InsertOneAsync(student);
        return student;
    }

    public async Task UpdateAsync(string id, Student student)
    {
        await _students.ReplaceOneAsync(student => student.Id == id, student);
    }

    public async Task DeleteAsync(string id)
    {
        await _students.DeleteOneAsync(student => student.Id == id);
    }
}
