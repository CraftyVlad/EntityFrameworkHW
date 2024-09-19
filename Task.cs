using Microsoft.EntityFrameworkCore;

public class Student
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<StudentCourse>? StudentCourses { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public string? Title { get; set; }

    public ICollection<StudentCourse>? StudentCourses { get; set; }
    public ICollection<CourseInstructor>? CourseInstructors { get; set; }
}

public class Instructor
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<CourseInstructor>? CourseInstructors { get; set; }
}

public class StudentCourse
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }
}

public class CourseInstructor
{
    public int Id { get; set; }

    public int InstructorId { get; set; }
    public Instructor? Instructor { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<CourseInstructor> CourseInstructors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=linqtoentities.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentCourse>()
            .HasAlternateKey(sc => new { sc.StudentId, sc.CourseId });

        modelBuilder.Entity<CourseInstructor>()
            .HasAlternateKey(ci => new { ci.InstructorId, ci.CourseId });
    }
}

public static class DataInitializer
{
    public static void Init(AppDbContext db)
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        if (db.Students.Any() ||
         db.Courses.Any() ||
         db.Instructors.Any())
        {
            return;
        }

        var students = new List<Student>
      {
       new Student { Name = "John Doe" },
       new Student { Name = "Jane Smith" },
       new Student { Name = "Alice Johnson" },
       new Student { Name = "Bob Brown" },
       new Student { Name = "Charlie Davis" },
       new Student { Name = "Diana Evans" },
       new Student { Name = "Eve Green" },
       new Student { Name = "Frank Harris" },
       new Student { Name = "Grace Ivers" },
       new Student { Name = "Hank Jones" },
       new Student { Name = "Ivy King" },
       new Student { Name = "Jack Lee" },
       new Student { Name = "Kara Moore" },
       new Student { Name = "Leo Nguyen" },
       new Student { Name = "Mona O'Neil" },
       new Student { Name = "Nina Perez" },
       new Student { Name = "Oscar Quinn" },
       new Student { Name = "Paul Roberts" },
       new Student { Name = "Quinn Stone" },
       new Student { Name = "Rita Taylor" }
      };

        var courses = new List<Course>
      {
       new Course { Title = "Mathematics" },
       new Course { Title = "Physics" },
       new Course { Title = "Chemistry" },
       new Course { Title = "Biology" },
       new Course { Title = "Computer Science" },
       new Course { Title = "History" },
       new Course { Title = "Philosophy" },
       new Course { Title = "Economics" },
       new Course { Title = "Psychology" },
       new Course { Title = "Sociology" },
       new Course { Title = "Political Science" },
       new Course { Title = "Literature" },
       new Course { Title = "Art History" },
       new Course { Title = "Music Theory" },
       new Course { Title = "Engineering" },
       new Course { Title = "Statistics" },
       new Course { Title = "Law" },
       new Course { Title = "Medicine" },
       new Course { Title = "Nursing" },
       new Course { Title = "Education" }
      };

        var instructors = new List<Instructor>
      {
       new Instructor { Name = "Prof. Emily Allen" },
       new Instructor { Name = "Dr. Michael Baker" },
       new Instructor { Name = "Prof. Sarah Clark" },
       new Instructor { Name = "Dr. David Davis" },
       new Instructor { Name = "Prof. Fiona Edwards" },
       new Instructor { Name = "Dr. George Foster" },
       new Instructor { Name = "Prof. Helen Gray" },
       new Instructor { Name = "Dr. Ian Howard" },
       new Instructor { Name = "Prof. Jessica Ivy" },
       new Instructor { Name = "Dr. Kevin Johnson" }
      };

        var studentCourses = new List<StudentCourse>
      {
       new StudentCourse { Student = students[0], Course = courses[0] },
       new StudentCourse { Student = students[0], Course = courses[1] },
       new StudentCourse { Student = students[1], Course = courses[2] },
       new StudentCourse { Student = students[1], Course = courses[3] },
       new StudentCourse { Student = students[2], Course = courses[4] },
       new StudentCourse { Student = students[2], Course = courses[5] },
       new StudentCourse { Student = students[3], Course = courses[6] },
       new StudentCourse { Student = students[3], Course = courses[7] },
       new StudentCourse { Student = students[4], Course = courses[8] },
       new StudentCourse { Student = students[4], Course = courses[9] },
       new StudentCourse { Student = students[5], Course = courses[10] },
       new StudentCourse { Student = students[5], Course = courses[11] },
       new StudentCourse { Student = students[6], Course = courses[12] },
       new StudentCourse { Student = students[6], Course = courses[13] },
       new StudentCourse { Student = students[7], Course = courses[14] },
       new StudentCourse { Student = students[7], Course = courses[15] },
       new StudentCourse { Student = students[8], Course = courses[16] },
       new StudentCourse { Student = students[8], Course = courses[17] },
       new StudentCourse { Student = students[9], Course = courses[18] },
       new StudentCourse { Student = students[9], Course = courses[19] }
      };

        var courseInstructors = new List<CourseInstructor>
      {
       new CourseInstructor { Course = courses[0], Instructor = instructors[0] },
       new CourseInstructor { Course = courses[1], Instructor = instructors[0] },
       new CourseInstructor { Course = courses[2], Instructor = instructors[1] },
       new CourseInstructor { Course = courses[3], Instructor = instructors[1] },
       new CourseInstructor { Course = courses[4], Instructor = instructors[2] },
       new CourseInstructor { Course = courses[5], Instructor = instructors[2] },
       new CourseInstructor { Course = courses[6], Instructor = instructors[3] },
       new CourseInstructor { Course = courses[7], Instructor = instructors[3] },
       new CourseInstructor { Course = courses[8], Instructor = instructors[4] },
       new CourseInstructor { Course = courses[9], Instructor = instructors[4] },
       new CourseInstructor { Course = courses[10], Instructor = instructors[5] },
       new CourseInstructor { Course = courses[11], Instructor = instructors[5] },
       new CourseInstructor { Course = courses[12], Instructor = instructors[6] },
       new CourseInstructor { Course = courses[13], Instructor = instructors[6] },
       new CourseInstructor { Course = courses[14], Instructor = instructors[7] },
       new CourseInstructor { Course = courses[15], Instructor = instructors[7] },
       new CourseInstructor { Course = courses[16], Instructor = instructors[8] },
       new CourseInstructor { Course = courses[17], Instructor = instructors[8] },
       new CourseInstructor { Course = courses[18], Instructor = instructors[8] },
       new CourseInstructor { Course = courses[19], Instructor = instructors[9] }
      };

        db.Students.AddRange(students);
        db.Courses.AddRange(courses);
        db.Instructors.AddRange(instructors);
        db.StudentCourses.AddRange(studentCourses);
        db.CourseInstructors.AddRange(courseInstructors);

        db.SaveChanges();
    }
}

class Program
{
    static void Main()
    {
        using (AppDbContext db = new AppDbContext())
        {
            DataInitializer.Init(db);
            
            // 1
            var coursesForJohnDoe = db.Students
                .Where(s => s.Name == "John Doe")
                .SelectMany(s => s.StudentCourses.Select(sc => sc.Course))
                .ToList();

            Console.WriteLine("Task 1: Courses for John Doe:");
            foreach (var course in coursesForJohnDoe)
            {
                Console.WriteLine(course.Title);
            }

            // 2
            var studentsWithMoreThanTwoCourses = db.Students
                .Where(s => s.StudentCourses.Count > 2)
                .ToList();

            Console.WriteLine("\nTask 2: Students attending more than two courses:");
            foreach (var student in studentsWithMoreThanTwoCourses)
            {
                Console.WriteLine(student.Name);
            }

            // 3
            var instructorsForMaths = db.Courses
                .Where(c => c.Title == "Mathematics")
                .SelectMany(c => c.CourseInstructors.Select(ci => ci.Instructor))
                .ToList();

            Console.WriteLine("\nTask 3: Instructors for Mathematics:");
            foreach (var instructor in instructorsForMaths)
            {
                Console.WriteLine(instructor.Name);
            }

            // 4
            var coursesWithNoStudents = db.Courses
                .Where(c => !c.StudentCourses.Any())
                .ToList();

            Console.WriteLine("\nTask 4: Courses with no students:");
            foreach (var course in coursesWithNoStudents)
            { 
                Console.WriteLine(course.Title); 
            }

            // 5
            var studentsForDrBaker = db.Instructors
                    .Where(i => i.Name == "Dr. Michael Baker")
                    .SelectMany(i => i.CourseInstructors
                        .SelectMany(ci => ci.Course.StudentCourses.Select(sc => sc.Student)))
                    .ToList();

            Console.WriteLine("\nTask 5: Students of Dr. Michael Baker:");
            foreach (var student in studentsForDrBaker)
            {
                Console.WriteLine(student.Name);
            }

            // 6
            var mostPopularCourses = db.Courses
                .OrderByDescending(c => c.StudentCourses.Count)
                .FirstOrDefault();

            Console.WriteLine("\nTask 6: Course with most students:");
            Console.WriteLine(mostPopularCourses?.Title);

            // 7
            var studentCountsPerCourse = db.Courses
                .Select(c => new
                {
                    CourseTitle = c.Title,
                    StudentCount = c.StudentCourses.Count
                })
                .ToList();

            Console.WriteLine("\nTask 7: Student counts per course:");
            foreach (var course in studentCountsPerCourse)
            {
                Console.WriteLine($"{course.CourseTitle}: {course.StudentCount} students");
            }

            // 8
            var coursesWithMultipleInstructors = db.Courses
                .Where(c => c.CourseInstructors.Count > 1)
                .ToList();

            Console.WriteLine("\nTask 8: Courses with more than one instructor:");
            foreach (var course in coursesWithMultipleInstructors)
            {
                Console.WriteLine(course.Title);
            }

            // 9
            var studentsWithNoCourses = db.Students
                .Where(s => !s.StudentCourses.Any())
                .ToList();

            Console.WriteLine("\nTask 9: Students not registered for any course:");
            foreach (var student in studentsWithNoCourses)
            {
                Console.WriteLine(student.Name);
            }

            // 10
            var instructorsWithOneCourse = db.Instructors
                .Where(i => i.CourseInstructors.Count == 1)
                .ToList();

            Console.WriteLine("\nTask 10: Instructors teaching only one course:");
            foreach (var instructor in instructorsWithOneCourse)
            {
                Console.WriteLine(instructor.Name);
            }

            // 11
            var averageStudentsPerCourse = db.Courses
                .Average(c => c.StudentCourses.Count);

            Console.WriteLine($"\nTask 11: Average number of students per course: {averageStudentsPerCourse}");

            // 12
            var coursesWithStudentsStartingWithA = db.Courses
                .Where(c => c.StudentCourses.Any(sc => sc.Student.Name.StartsWith("A")))
                .ToList();

            Console.WriteLine("\nTask 12: Courses with students whose name starts with 'A':");
            foreach (var course in coursesWithStudentsStartingWithA)
            {
                Console.WriteLine(course.Title);
            }

            // 13
            var instructorsForPhysics = db.Courses
                .Where(c => c.Title == "Physics")
                .SelectMany(c => c.CourseInstructors.Select(ci => ci.Instructor))
                .ToList();

            Console.WriteLine("\nTask 13: Instructors for Physics:");
            foreach (var instructor in instructorsForPhysics)
            {
                Console.WriteLine(instructor.Name);
            }

            // 14
            var coursesForJohnAndAlice = db.Courses
                .Where(c => c.StudentCourses.Any(sc => sc.Student.Name == "John Doe") &&
                c.StudentCourses.Any(sc => sc.Student.Name == "Alice Smith"))
                .ToList();

            Console.WriteLine("\nTask 14: Courses for both John Doe and Alice Smith:");
            foreach (var course in coursesForJohnAndAlice)
            {
                Console.WriteLine(course.Title);
            }

            var studentsWithDuplicateCourses = db.Students
                .Where(s => s.StudentCourses.GroupBy(sc => sc.CourseId)
                .Any(g => g.Count() > 1))
                .ToList();

            Console.WriteLine("\nTask 15: Students registered for the same course more than once:");
            foreach (var student in studentsWithDuplicateCourses)
            {
                Console.WriteLine(student.Name);
            }
        }
    }
}