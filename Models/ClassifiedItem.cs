using System.ComponentModel.DataAnnotations;

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Bu sınıf Classifieds System için ilan (item) modelimizi tanımlar.
// DataAnnotations ile doğrulama kuralları ekledik.

public class ClassifiedItem
{
    public int Id { get; set; } // Benzersiz Id (InMemoryRepository otomatik atar)

    [Required]
    public string Title { get; set; } = "Unknown Title"; 
    // Başlık boş bırakılamaz (formda zorunlu)

    public string Description { get; set; } = "Unknown Description";
    // Açıklama (zorunlu değil, default değer var)

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; } = 1;
    // Fiyat en az 0.01 olmalı (negatif fiyat engellenir)

    public string Seller { get; set; } = "Unknown Seller";
    // İlanı veren kişinin e-posta adresi burada tutulur (genelde login user email)
}
