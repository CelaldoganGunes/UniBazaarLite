using System;
using System.ComponentModel.DataAnnotations;

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Bu sınıf, Subsystem A (Events System) için etkinlik modelimizi tanımlar.
// DataAnnotations ile formda tarih alanını daha doğru göstermeyi sağladık.

public class Event
{
    public int Id { get; set; } 
    // Benzersiz Id (InMemoryRepository tarafından otomatik atanır)

    public string Title { get; set; } = "";
    // Etkinlik başlığı (zorunluluğu biz controller'da veya view'da kontrol ediyoruz)

    public string Description { get; set; } = "";
    // Etkinlik açıklaması

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    // Bu DataAnnotation, ASP.NET Core'un formda HTML5 date picker çıkarmasını sağlar
    // Aynı zamanda validasyon mesajlarında 'geçerli bir tarih girin' şeklinde uyarı döner
}
