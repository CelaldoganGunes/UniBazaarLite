using System.Collections.Generic;

// Yapımcı: Hüseyin Kaplan
// Bu interface, repository tasarım deseninin temel sözleşmesini belirler.
// Generic olarak çalışır ve T tipindeki nesneler üzerinde CRUD işlemleri sağlar.

public interface IRepository<T>
{
    IEnumerable<T> GetAll(); // Tüm kayıtları getir
    T GetById(int id);       // Belirli Id'ye sahip kaydı getir
    void Add(T item);        // Yeni kayıt ekle
    void Update(T item);     // Var olan kaydı güncelle
    void Delete(int id);     // Id ile kaydı sil
}
