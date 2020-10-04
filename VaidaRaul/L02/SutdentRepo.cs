using System.Collections.Generic;

namespace studentsAPI
{
    public static class StudentRepo
    {
        private static List<Student> students = new List<Student>();

        public static void insert(Student data)
        {
            data.Id = students.Count;
            students.Add(data);
        }

        public static List<Student> getAll()
        {
            return students;
        }

    }
}