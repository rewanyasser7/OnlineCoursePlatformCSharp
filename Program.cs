using System;
namespace OnlineCouesePlatform
{
    enum EnrollmentStatus
    {
        Active,
        Completed,
        Dropped
    }
    struct Rating
    {
        public string myRate;
        public string comment;
        public void rate()
        {
            Console.WriteLine("Write Your Rate from (1 to 5): ");
            myRate = Console.ReadLine();
           comment = "Thanks for rating us!";
            Console.WriteLine(comment);
        }
    }
    abstract class Person
    {
        public string name;
        public double ID;
        public string answer;
        abstract public void DisplayInfo();
    }
    class Student : Person, IReprtable
    {
        private double gpa;
        public int enrolledCourseCode;
        public EnrollmentStatus Status;

        public double GPA
        {
            get { return gpa; }
            set {
                if (value > 0 && value <= 4) { gpa = value; } 
                else { Console.WriteLine("Invalid Data(Must be between 1 and 4)"); }
            }
        }
        public override void DisplayInfo()
        {
            Console.WriteLine("Student's name: " + name);
            Console.WriteLine("Student's ID: " + ID);
            Console.WriteLine("Student's GPA: " + gpa);
            Console.WriteLine("Enrolled course code: "+ enrolledCourseCode);
            Console.WriteLine("Enrollment Status: "+ Status);
        }
        public void Report()
        {
            Console.WriteLine("Write Your Report: ");
            answer = Console.ReadLine();
        }
    }
    class Teacher : Person, IReprtable
    {
        public string Specialty;
        public override void DisplayInfo()
        {
            Console.WriteLine("Teacher's name: " + name);
            Console.WriteLine("Teacher's ID: " + ID);
            Console.WriteLine("Teacher's Specialty: " + Specialty);
        }
        public void Report()
        {
            Console.WriteLine("Write Your Report: ");
            answer = Console.ReadLine();
        }
    }
    public class Lesson
    {
        public string title;
        public int duration;
    }
      public class Course
    {
        public string name;
        public int code;
        public int duration;
        private Lesson[] lessons;
        private static int counter = 0;
        public int lessonCount;
        public Course(string name, int code, int duration, int i)
        {
            this.name = name;
            this.code = code;
            this.duration = duration;
            this.lessons = new Lesson[i];
            this.lessonCount = i;
            counter++;
        }
        //indexer to access the lessons by number
        public Lesson this[int i]
        {
            get { return lessons[i]; }
            set { lessons[i] = value; }
        }
        public static int GetCount() {  return counter; }
        //operator overloading to compare two objects
        public static bool operator ==(Course code1, Course code2)
        {
            if (ReferenceEquals(code1, null) || ReferenceEquals(code2, null)) return false;
            return code1.code == code2.code;
        }
        public static bool operator !=(Course code1, Course code2)
        {
            return !(code1 == code2);
        }
        public override bool Equals(object obj)
        {
            if (obj is Course other)
                return this.code == other.code;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public virtual void StartCourse()
        {
            Console.WriteLine("Enrolling in course: "+ name);
        }
    }
    public class PaidCourse: Course
    {
        private double price;
        public double Price
        {
            get { return price; }
            set { if (value > 0) price = value; }
        }
        public PaidCourse(string name,int code,int duration,int i, double price)
            : base(name,code,duration,i)
        {
            this.price = price;  
        }
        public override void StartCourse()
        {
            double total = price - (price * 20/100);
            Console.WriteLine("Enrolling in: " + name);
            Console.WriteLine("Original price: " + price + " pounds.");
            Console.WriteLine("After 20% discount: " + total + " pounds.");
        }
    }
    public class FreeCourse : Course
    {
        public FreeCourse(string name, int code, int duration,int i)
            :base(name, code, duration,i) { }
      
        public override void StartCourse()
        {
            Console.WriteLine("Enroll this course for free!");
        }
    }
    //interface, like a contract for submetting reports
    interface IReprtable
    {
        void Report();
    }
     
        internal class Program
        {
          //a method for Creating a course with its lessons
          public static Course CreateCourse()
         {
            Console.WriteLine("Course Name: ");
            string Cname = Console.ReadLine();
            Console.WriteLine("Course Code: ");
            int Ccode = int.Parse(Console.ReadLine());
            Console.WriteLine("Number of lessons: ");
            int i = int.Parse(Console.ReadLine());
            Console.WriteLine("Duration in Hours: ");
            int hours = int.Parse(Console.ReadLine());
            

            Console.WriteLine("You want Free or Paid Course?\n if Free--> Wrire 1\n if Paid --> Write 2");
            int answered = int.Parse(Console.ReadLine());

            Course newCourse = null;

            if (answered == 1)
            {
                newCourse = new FreeCourse(Cname, Ccode, hours, i);
            }
            else if (answered == 2)
            {
                Console.WriteLine("Price: ");
                double Cprice = double.Parse(Console.ReadLine());
                newCourse = new PaidCourse(Cname, Ccode, hours, i, Cprice);
            }
            else { Console.WriteLine("Invalid choice"); return null; }

           //using the indexer to fill the lessons
            for (int j = 0; j < i; j++)
            {
                Console.WriteLine("Lesson " + (j + 1) + " title: ");
                string ltitle = Console.ReadLine();
                Console.WriteLine("Lesson " + (j + 1) + " duration in minutes: ");
                int lduration = int.Parse(Console.ReadLine());

                //creating the lesson
                Lesson lesson = new Lesson();
                lesson.title = ltitle;
                lesson.duration = lduration;
                newCourse[j] = lesson;
            }
            return newCourse;
            }
        // Creating student object
         public static Student createStudent()
        {
            Console.WriteLine("Student's name: ");
            string Sname = Console.ReadLine();
            Console.WriteLine("Student's ID: ");
            double ID = double.Parse(Console.ReadLine());
            Console.WriteLine("Student's GPA: ");
            double GPA = double.Parse(Console.ReadLine());

            Student s = new Student();
            s.name = Sname;
            s.ID = ID;
            s. GPA = GPA;
            return s;
        }
        // Creating a teacher object
        public static Teacher CreateTeacher()
        {
            Console.WriteLine("Teacher's Name: ");
            string Tname = Console.ReadLine();
            Console.WriteLine("Teacher's ID: ");
            double ID = double.Parse(Console.ReadLine());
            Console.WriteLine("Teacher's Specialty: ");
            string specialty = Console.ReadLine();

            Teacher t = new Teacher();
            t.name = Tname;
            t.ID = ID;
            t.Specialty = specialty;
            return t;
        }
        //  fills arrays with test data, just foe the demo
        static void LoadSampleData(Course[] courses, ref int count,
                                   Student[] students, ref int sCount,
                                   Teacher[] teachers, ref int tCount)
        {
            // Sample free course
            Course c1 = new FreeCourse("Math", 101, 10, 2);
            c1[0] = new Lesson { title = "Intro", duration = 30 };
            c1[1] = new Lesson { title = "Algebra", duration = 45 };
            courses[count++] = c1;

            // Sample paid course
            Course c2 = new PaidCourse("Programming", 202, 20, 3, 500);
            c2[0] = new Lesson { title = "Variables", duration = 40 };
            c2[1] = new Lesson { title = "Loops", duration = 50 };
            c2[2] = new Lesson { title = "Functions", duration = 60 };
            courses[count++] = c2;

            // Sample student
            Student s = new Student();
            s.name = "Rewan";
            s.ID = 1001;
            s.GPA = 3.8;
            students[sCount++] = s;

            // Sample teacher
            Teacher t = new Teacher();
            t.name = "Dr. Ahmed";
            t.ID = 5001;
            t.Specialty = "Computer Science";
            teachers[tCount++] = t;
        }
        public static void menu()
           {
            Course[] courses = new Course[10];
            int coursesCount = 0;
            Student[] students = new Student[10];
            int studentCount = 0;
            Teacher[] teachers = new Teacher[10];
            int teachersCount = 0;

            LoadSampleData(courses, ref coursesCount, students, ref studentCount,teachers, ref teachersCount);
            while (true) {
                Console.WriteLine("====Online Course Platform====");
                Console.WriteLine("1.Add A Course");
                Console.WriteLine("2.Show Course Lessons");
                Console.WriteLine("3.Enroll in Course");
                Console.WriteLine("4.Add A Student");
                Console.WriteLine("5.Show Student Info");
                Console.WriteLine("6.Compare Two Courses");
                Console.WriteLine("7.Rate A Course");
                Console.WriteLine("8.Submit A Report");
                Console.WriteLine("9.Add A Teacher");
                Console.WriteLine("10.Exit");
                Console.WriteLine("============================");
                Console.WriteLine("Choose An Option: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: //add a course
                        Course newCourse = CreateCourse(); //calling the CreateCourse method to get the data and create the course object                      
                        if (newCourse != null)
                        {
                            coursesCount++; //incrementing the counter for each course object created
                            Console.Write("Course added successfully! ");
                            Console.WriteLine("Total Courses: " + Course.GetCount());
                        }
                            break;

                    case 2: //show course lessons
                        Console.WriteLine("Course code you want to see its lessons: ");
                        int Code2 = int.Parse(Console.ReadLine());
                        bool foundLesson = false;
                        for (int j = 0; j < coursesCount; j++)
                        {
                            if (courses[j] != null && courses[j].code == Code2)
                            {
                                foundLesson = true;
                                Console.WriteLine("Lessons in: " + courses[j].name);
                                for (int k=0; k< courses[j].lessonCount; k++)
                                {
                                    Console.WriteLine("Lesson's title is "+ courses[j][k].title);
                                    Console.WriteLine("Lesson's duration is "+ courses[j][k].duration +"mins");
                                }
                            }
                        }
                        if(!foundLesson)
                            Console.WriteLine("Corse not found.");
                        break;

                  case 3: //enroll student in course
                        Console.WriteLine("Enter Student ID: ");
                        double enrollID = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Course Code: ");
                        int enrollCode = int.Parse(Console.ReadLine());

                        Student foundStudent = null; //setting the intial values for null
                        Course foundCourse = null;

                        for (int j = 0; j < studentCount; j++)
                            if (students[j].ID == enrollID)//checking if this requierd student
                                foundStudent = students[j]; //set the intial value for the student we found

                        //same with the course
                        for (int j = 0; j < coursesCount; j++)
                            if (courses[j] != null && courses[j].code == enrollCode)
                                foundCourse = courses[j];

                        if (foundStudent != null && foundCourse != null)//check if they have real value
                        {
                            foundCourse.StartCourse(); // free or paid behavior(polymorphisim)
                            foundStudent.Status = EnrollmentStatus.Active; //change the student status to active
                            foundStudent.enrolledCourseCode = enrollCode; //set his enrollCode
                            Console.WriteLine(foundStudent.name + " enrolled! Status: Active");
                        }
                        else
                        {
                            Console.WriteLine("Student or Course not found.");
                        }
                        break;
                        case 4: //add a student
                        students[studentCount] = createStudent();
                        studentCount++;
                        Console.WriteLine("Student Added!");
                        break;

                        case 5: // show student info
                        Console.WriteLine("Student's ID: ");
                        double stdID = double.Parse(Console.ReadLine());
                        bool foundStd = false;
                        for (int i = 0; i < studentCount; i++)
                        {
                            if (students[i] != null && students[i].ID == stdID)
                            {
                                foundStd = true;
                                students[i].DisplayInfo();
                            }
                        }
                        if (!foundStd) Console.WriteLine("Student not found.");
                        break;
                       
                        case 6: //compare two courses
                        Console.WriteLine("Enter Course 1 Code: ");
                        int c1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Course 2 Code: ");
                        int c2 = int.Parse(Console.ReadLine());

                        Course found1 = null, found2 = null; //setting the values of each for null first
                        for( int i = 0; i<coursesCount; i++) 
                        {
                            if (courses[i].code == c1) found1 = courses[i]; // check if found
                            if(courses[i].code == c2) found2 = courses[i];
                        }
                        if(found1 != null && found2 !=null) //if it's founf and it's not null
                        {
                            if (found1 == found2) // check if they are equal
                                Console.WriteLine("Same Course.");
                            else
                                Console.WriteLine("Diffrent Courses");
                        }
                        break;
                        case 7:// rate a course
                        Console.WriteLine("Course Code you want to rate: ");
                        int Ccode = int.Parse(Console.ReadLine());
                        bool foundRate = false;
                        for (int i = 0;i<coursesCount;i++)
                        {
                            if (courses[i] != null && courses[i].code == Ccode)
                            {
                                foundRate = true;
                                Rating r; //using the rating struct
                                r.myRate = "";
                                r.comment = "";
                                r.rate();
                            }
                        }
                        if (!foundRate) Console.WriteLine("Course not found.");
                        break;
                        case 8: //sumbit a report
                        Console.WriteLine("Student or Teacher?\n if Student--> Wrire 1\n if Teacher --> Write 2");
                        int answer= int.Parse(Console.ReadLine());
                        if(answer==1)
                        {
                            Console.WriteLine("Your ID: ");
                            double sID = double.Parse(Console.ReadLine());
                            bool foundAns = false;
                            for(int i = 0; i<studentCount ; i++)
                            {
                                if (students[i] != null && students[i].ID == sID)
                                {
                                    foundAns = true;
                                    students[i].Report();
                                    Console.WriteLine("Report Submitted by: " + students[i].name);
                                }
                            }
                         if (!foundAns) Console.WriteLine("Student not found.");
                        }
                        else if(answer==2)
                        {
                            Console.WriteLine("Your ID: ");
                            int tID = int.Parse(Console.ReadLine());
                            bool foundTAns = false;
                            for(int j = 0; j<teachersCount ; j++)
                            {
                                if (teachers[j] != null && teachers[j].ID == tID)
                                {
                                    foundTAns = true;
                                    teachers[j].Report();
                                    Console.WriteLine("Report Submitted by: "+teachers[j].name);
                                }
                            }
                            if (!foundTAns) Console.WriteLine("Teacher not found.");
                        }
                        break;
      
                        case 9://add a teacher
                        teachers[teachersCount] = CreateTeacher();
                        teachersCount++;
                        Console.WriteLine("Teacher added successfully!");
                        break ;

                        case 10://exit
                        Console.WriteLine("GoodBye!");
                        return;

                        default:
                        Console.WriteLine("Invalid option, Try again.");
                        break;
                }
        }
        }
        static void Main(string[] args)
            {
            menu();

            }
        }
}
    
