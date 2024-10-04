# Changelog -  Fiever

---

## Table of Contents

- [Unreleased](#unreleased)
- [Released](#released)
  - [v0.1.0 - 2024-04-10](#v010---2024-04-10)

---

## Unreleased



---

## Released

### v0.1.0 - 2024-04-10

- Added new entities: **University**, **Course**, **Passport**, and **StudentCourse** to represent various relationships in our domain model.
  - **University** entity represents educational institutions that students can be enrolled in.
  - **Course** entity represents the courses offered by universities.
  - **Passport** entity represents passport details linked to a student.
  - **StudentCourse** represents the many-to-many relationship between **Student** and **Course** entities.

- Updated repositories to handle CRUD operations for new entities:
  - Added **UniversityRepository**, **CourseRepository**, **PassportRepository**, and **StudentCourseRepository** to implement repository patterns for respective entities.
  - **UniversityRepository** and **CourseRepository** manage operations related to universities and courses respectively.
  - **PassportRepository** provides functionality for managing student passports, including operations to retrieve a passport by student ID.
  - **StudentCourseRepository** handles many-to-many relationships between students and courses, including enrolling and unenrolling students from courses.

- Created application services for new entities to manage business logic:
  - **UniversityAppService**, **CourseAppService**, **PassportAppService**, and **StudentCourseAppService** were added to handle application-specific operations for respective entities.
  - Added methods in **PassportAppService** to retrieve passport information based on student ID.
  - Updated **StudentCourseAppService** to handle enrollments and checks for student-course relationships.

- Updated API controllers to expose new endpoints:
  - **UniversityController**, **CourseController**, and **PassportController** were added to handle HTTP requests for CRUD operations on universities, courses, and passports.
  - Added endpoint in **PassportController** to fetch passport information by student ID.

- Initial project configuration
  - Create empty dotnet 9.0 solution which contains:
    - **API** web api project
      - Contain our main entry point and API main configurations
      - Contain all controllers, which contains our endpoints
    - **Domain** class library project
      - Contain our entities and repository interfaces
    - **Application** class library project
      - Contain our business logic
    - **Infrastructure** class library project
      - Contain all services that are related to external APIs
      - Contain database context
      - Implement repositories
  - Configure **http session** and **Redis** to manage caching sessions on server or local development computer
  - Configure database context using EntityFramework and MongoDB driver
    - Create **AppDbContext** to manage rules and how entity framework interact with database
  - Create API versioning, currently *v1.0*
  - Testing tools
    - Create **GoldenBack.Tests** project
      - Add **xUnit** testing framework
      - Add **Moq** mocking library
      - Add **FluentAssertions** for fluent assertions

---