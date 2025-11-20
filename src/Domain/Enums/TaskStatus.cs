namespace EnterpriseTaskManagement.Domain.Enums;

public enum TaskStatus
{
    Todo = 1, //Yapılacak
    Inprogress = 2, //Devam ediyor
    Review = 3, //İncelemede
    Testing = 4, // Test ediliyor
    Done = 5, //Tamamlandı
    Blocked = 6 // Engellendi

}
