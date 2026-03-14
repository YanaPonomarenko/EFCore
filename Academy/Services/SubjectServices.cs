using Academy.Context;
using Academy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Services;

public class SubjectServices
{
    private readonly AcademyDbContext _db = new();

    public void AddSubject(string name, string? description)
    {
        _db.Subjects.Add(new Subject { Name = name, Description = description });
        _db.SaveChanges();
    }

    public List<Subject> GetAllSubjects() => _db.Subjects.ToList();

    public Subject? GetSubjectById(int id) => _db.Subjects.Find(id);

    public void UpdateSubject(int id, string name, string? description)
    {
        var s = _db.Subjects.Find(id);
        if (s != null)
        {
            s.Name = name;
            s.Description = description;
            _db.SaveChanges();
        }
    }

    public void DeleteSubject(int id)
    {
        var hasGrades = _db.Grades.Any(g => g.SubjectId == id);
        if (hasGrades) throw new Exception("Cannot delete: Subject has grades!");

        var s = _db.Subjects.Find(id);
        if (s != null)
        {
            _db.Subjects.Remove(s);
            _db.SaveChanges();
        }
    }
}
