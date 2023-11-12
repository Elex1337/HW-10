using System;
using System.Collections;


public class Person
{
    public string name { get; set; }
    public int age { get; set; }
    public virtual void Print()
    {
        Console.WriteLine($"{name} {age}");
    }
    public string ToString()
    {
        return $"{name} {age}";
    }
    public override bool Equals(object obj)
    {
        if (obj is Person otherPerson)
        {
            return name == otherPerson.name && age == otherPerson.age;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return name.GetHashCode() & age.GetHashCode();
    }

}
public class Student : Person
{
    public string Id { get; set; }
    public Teacher teacher { get; set; }
    public virtual void Print()
    {
        base.Print();
        Console.WriteLine($"ID Студента {Id}");
    }
    public string ToString()
    {
        return $"ID Студента {Id}";
    }
    public override bool Equals(object obj)
    {
        if (obj is Student otherStudent)
        {
            return base.Equals(obj) && Id == otherStudent.Id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() & Id.GetHashCode();
    }
}

public class Teacher : Person
{
    public string discipline { get; set; }
    public List<Student> Students { get; set; }
    public Teacher()
    {
        Students = new List<Student>();
    }
    public void Print()
    {
        base.Print();
        Console.WriteLine("Students: ");
        for (int i = 0; i < Students.Count; i++)
        {
            Console.WriteLine($"- {Students[i].ToString()}");
        }
    }
    public override bool Equals(object obj)
    {
        if (obj is Teacher otherTeacher)
        {
            return base.Equals(obj) && discipline == otherTeacher.discipline;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ discipline.GetHashCode();
    }

}
public class StudentWithAdvisor : Student
{
    public Teacher Advisor { get; set; }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Advisor: {Advisor.ToString()}");
    }


    public override bool Equals(object obj)
    {
        if (obj is StudentWithAdvisor otherStudent)
        {
            return base.Equals(obj) && Advisor.Equals(otherStudent.Advisor);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ Advisor.GetHashCode();
    }
}

class Program
{

    public static void Main()
    {
        Person[] people = new Person[]
        {
            new Person {name = "Nurbolat Zhursinbek ", age = 19},
            new Student{name = "Nurbolat Zhursinbek ", Id = "12345"},
            new Student{name = "Nurbolat Zhursinbek ", Id = "12345"},
            new Student{name = "Nurbolat Zhursinbek ", Id = "12345"},
            new Student{name = "Nurbolat Zhursinbek ", Id = "12345"},
            new Student{name = "Nurbolat Zhursinbek ", Id = "12345"},
            new Student{name = "Nurbolat Zhursinbek ", Id = "12345"},
            new Teacher{name = "Nurbolat Nurlanuly ", age = 35, discipline = "Computer Science"}


        };
        foreach (var person in people)
        {
            person.Print();
            Console.WriteLine("------------------------------");
        }

        int personCount = 0, studentCount = 0, teacherCount = 0;

        foreach (var person in people)
        {
            if (person is Student)
            {
                studentCount++;
                if (person is StudentWithAdvisor)
                {
                    StudentWithAdvisor studentWithAdvisor = person as StudentWithAdvisor;
                    if (studentWithAdvisor != null)
                    {
                        Console.WriteLine($"StudentWithAdvisor: {studentWithAdvisor.ToString()}");
                    }
                }
            }
            else if (person is Teacher)
            {
                teacherCount++;
            }

            if (person.GetType() == typeof(Person))
            {
                Console.WriteLine($"Person: {person.ToString()}");
            }
        }

        Console.WriteLine($"Total Students: {studentCount}");
        Console.WriteLine($"Total Teachers: {teacherCount}");

        foreach (var person in people)
        {
            if (person is Student student)
            {
                Console.WriteLine($"Transferring student {student.name} to the next course.");
            }
        }
    }

}
