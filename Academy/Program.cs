using Academy.Services;
using System;

var subjectService = new SubjectServices();
var gradeService = new GradeServices();

while (true)
{
    Console.WriteLine("\n ACADEMY MANAGEMENT SYSTEM ");
    Console.WriteLine("1. Add Subject");
    Console.WriteLine("2. Show All Subjects");
    Console.WriteLine("3. Update Subject");
    Console.WriteLine("4. Delete Subject");
    Console.WriteLine("5. Add Grade");
    Console.WriteLine("6. Show Student Grades");
    Console.WriteLine("7. Update Grade");
    Console.WriteLine("8. Delete Grade");
    Console.WriteLine("0. Exit");
    Console.Write("\nSelect an option: ");

    var choice = Console.ReadLine();
    if (choice == "0") break;

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Name: "); string name = Console.ReadLine()!;
                Console.Write("Description: "); string? desc = Console.ReadLine();
                subjectService.AddSubject(name, desc);
                Console.WriteLine("Subject added!");
                break;

            case "2":
                var subjects = subjectService.GetAllSubjects();
                foreach (var s in subjects) Console.WriteLine($"ID: {s.Id} | {s.Name} ({s.Description})");
                break;

            case "4":
                Console.Write("Enter Subject ID to delete: ");
                int delSubId = int.Parse(Console.ReadLine()!);
                subjectService.DeleteSubject(delSubId);
                Console.WriteLine("Subject deleted!");
                break;

            case "5":
                Console.Write("Student ID: "); int stId = int.Parse(Console.ReadLine()!);
                Console.Write("Subject ID: "); int subId = int.Parse(Console.ReadLine()!);
                Console.Write("Grade (1-12): "); int val = int.Parse(Console.ReadLine()!);
                gradeService.AddGrade(stId, subId, val);
                Console.WriteLine("Grade added!");
                break;

            case "6":
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine()!);
                var grades = gradeService.GetGradesForStudent(studentId);
                foreach (var g in grades) Console.WriteLine($"Grade ID: {g.Id} | Subject: {g.SubjectId} | Value: {g.Value}");
                break;

            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }
}

