using Academy.Context;
using Academy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Services;

public class GradeServices
{
    private readonly AcademyDbContext _db = new();

    public void AddGrade(int studentId, int subjectId, int value)
    {
        if (value < 1 || value > 12) throw new Exception("Grade must be 1-12");
        if (!_db.Subjects.Any(s => s.Id == subjectId)) throw new Exception("Subject not found");

        _db.Grades.Add(new Grade { StudentId = studentId, SubjectId = subjectId, Value = value });
        _db.SaveChanges();
    }

    public List<Grade> GetGradesForStudent(int studentId) =>
        _db.Grades.Where(g => g.StudentId == studentId).ToList();

    public List<Grade> GetGradesForSubject(int subjectId) =>
        _db.Grades.Where(g => g.SubjectId == subjectId).ToList();

    public void UpdateGrade(int gradeId, int newValue)
    {
        if (newValue < 1 || newValue > 12) throw new Exception("Grade must be 1-12");
        var g = _db.Grades.Find(gradeId);
        if (g != null) { g.Value = newValue; _db.SaveChanges(); }
    }

    public void DeleteGrade(int gradeId)
    {
        var g = _db.Grades.Find(gradeId);
        if (g != null) { _db.Grades.Remove(g); _db.SaveChanges(); }
    }
}
