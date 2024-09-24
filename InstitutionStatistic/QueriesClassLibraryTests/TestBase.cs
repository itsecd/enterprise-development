using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models;

namespace InstitutionStatistic.Query.Test;

/// <summary>
/// Базовый класс для тестов
/// </summary>
public class TestBase
{
    protected List<Institution> Institutions = new List<Institution>();

    protected List<Speciality> Specialities = new List<Speciality>();

    /// <summary>
    /// ctor
    /// </summary>
    public TestBase() 
    {

        #region Rectors
        var rector1 = new Rector("Иванов A.Ю", ScientificDegree.Candidate, AcademicRank.Docent);
        var rector2 = new Rector("Петров В.А", ScientificDegree.Doctor, AcademicRank.Professor);
        var rector3 = new Rector("Сидоров Н.П", ScientificDegree.Candidate, AcademicRank.Docent);
        var rector4 = new Rector("Бобров А.М", ScientificDegree.Doctor, AcademicRank.Professor);
        #endregion

        #region Faculties
        var faclt1 = new Faculty("FC1", null, new List<Department> { });
        var faclt2 = new Faculty("FC2", null, new List<Department> { });
        var faclt3 = new Faculty("FC3", null, new List<Department> { });
        var faclt4 = new Faculty("FC4", null, new List<Department> { });
        var faclt5 = new Faculty("FC5", null, new List<Department> { });
        #endregion

        #region Departments
        var department1 = new Department("ГИИБ", null, new List<Group> { });
        var department2 = new Department("ИСТ", null, new List<Group> { });
        var department3 = new Department("ЛБС", null, new List<Group> { });
        var department4 = new Department("TEST1", null, new List<Group> { });
        var department5 = new Department("TEST2", null, new List<Group> { });
        var department6 = new Department("TEST3", null, new List<Group> { });
        #endregion

        #region Specialities
        var speciality1 = new Speciality("SPEC1", "123456", new List<Group> { });
        var speciality2 = new Speciality("SPEC2", "234567", new List<Group> { });
        var speciality3 = new Speciality("SPEC3", "345678", new List<Group> { });
        var speciality4 = new Speciality("SPEC4", "456789", new List<Group> { });
        var speciality5 = new Speciality("SPEC5", "567890", new List<Group> { });
        var speciality6 = new Speciality("SPEC6", "678901", new List<Group> { });
        #endregion

        #region Groups
        var group1 = new Group("6412-100503", department1, speciality1);
        var group2 = new Group("6411-100503", department2, speciality2);
        var group3 = new Group("6413-100503", department2, speciality3);


        var group4 = new Group("6414-100503", department3, speciality1);
        var group5 = new Group("5101-100503", department3, speciality2);
        var group6 = new Group("5102-100503", department3, speciality3);
        var group7 = new Group("5103-100503", department3, speciality4);
        var group8 = new Group("5104-100503", department3, speciality1);

        var group9 = new Group("3221-100503", department4, speciality1);
        var group10 = new Group("3222-100503", department4, speciality2);
        var group11 = new Group("3223-100503", department4, speciality3);
        var group12 = new Group("3223-100503", department5, speciality4);
        var group13 = new Group("7405-100503", department5, speciality4);

        var group14 = new Group("7406-100503", department6, speciality3);
        var group15 = new Group("7407-100503", department6, speciality2);
        var group16 = new Group("7408-100503", department6, speciality4);

        var group17 = new Group("7409-100503", department6, speciality5);
        #endregion

        #region Institutions
        var inst1 = new Institution(
            "СГАУ",
            "test register1",
            "test address1",
            rector1,
            new List<Faculty> { },
            BuildingOwnership.Federal,
            InstitutionOwnership.Municipality);

        var inst2 = new Institution(
            "САМГТУ", 
            "test register2", 
            "test address2", 
            rector2,
            new List<Faculty> { },
            BuildingOwnership.Municipality,
            InstitutionOwnership.Municipality);

        var inst3 = new Institution(
            "ПГУТИ", 
            "test register3", 
            "test address3",
            rector3,
            new List<Faculty> { },
            BuildingOwnership.Personal,
            InstitutionOwnership.Personal);

        var inst4 = new Institution(
            "САМГМУ", 
            "test register4", 
            "test address4",
            rector4,
            new List<Faculty> { },
            BuildingOwnership.Federal,
            InstitutionOwnership.Personal);
        #endregion

        #region initAllValues
        //первый институт
        inst1.Faculties.Add(faclt1);
        inst1.Faculties.Add(faclt2);

        faclt1.Institution = inst1;
        faclt2.Institution = inst1;

        faclt1.Departments.Add(department1);
        faclt2.Departments.Add(department2);

        department1.Faculty = faclt1;
        department2.Faculty = faclt2;

        department1.Groups.Add(group1);
        department2.Groups.Add(group2);
        department2.Groups.Add(group3);


        speciality1.Groups.Add(group1);
        speciality2.Groups.Add(group2);
        speciality3.Groups.Add(group3);

        //воторой институт
        inst2.Faculties.Add(faclt3);

        faclt3.Institution = inst2;

        faclt3.Departments.Add(department3);

        department3.Faculty = faclt3;

        department3.Groups.Add(group4);
        department3.Groups.Add(group5);
        department3.Groups.Add(group6);
        department3.Groups.Add(group7);
        department3.Groups.Add(group8);

        speciality1.Groups.Add(group4);
        speciality1.Groups.Add(group8);
        speciality2.Groups.Add(group5);
        speciality4.Groups.Add(group7);
        speciality3.Groups.Add(group6);

        //третий институт
        inst3.Faculties.Add(faclt4);

        faclt4.Institution = inst3;

        faclt4.Departments.Add(department4);
        faclt4.Departments.Add(department5);

        department4.Faculty = faclt4;
        department5.Faculty = faclt4;

        department4.Groups.Add(group9);
        department4.Groups.Add(group10);
        department4.Groups.Add(group11);
        department5.Groups.Add(group12);
        department5.Groups.Add(group13);

        speciality1.Groups.Add(group9);
        speciality2.Groups.Add(group10);
        speciality3.Groups.Add(group11);
        speciality4.Groups.Add(group12);
        speciality4.Groups.Add(group13);

        //четвертый институт
        inst4.Faculties.Add(faclt5);

        faclt5.Institution = inst4;

        faclt5.Departments.Add(department6);

        department6.Faculty = faclt4;

        department6.Groups.Add(group14);
        department6.Groups.Add(group15);
        department6.Groups.Add(group16);
        department6.Groups.Add(group17);

        speciality3.Groups.Add(group14);
        speciality2.Groups.Add(group15);
        speciality4.Groups.Add(group16);
        speciality5.Groups.Add(group17);
        //Добавляем институты и специальности...
        Institutions.Add(inst1);
        Institutions.Add(inst2);
        Institutions.Add(inst3);
        Institutions.Add(inst4);
        Specialities.Add(speciality1);
        Specialities.Add(speciality2);
        Specialities.Add(speciality3);
        Specialities.Add(speciality4);
        Specialities.Add(speciality5);
        Specialities.Add(speciality6);
        #endregion
    }
}
