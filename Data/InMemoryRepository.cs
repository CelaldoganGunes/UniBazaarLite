using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// Yapımcı: Hüseyin Kaplan
// Bu sınıf generic bir bellek içi (in-memory) repository'dir.
// Olayları ve ilanları (Event, ClassifiedItem) bellekte saklar.

public class InMemoryRepository<T> : IRepository<T> where T : class
{
    private readonly List<T> _items = new(); // Verileri saklayan liste
    private int _nextId = 1; // Otomatik artan Id için sayaç

    private readonly PropertyInfo _idProperty; // T tipi üzerinde "Id" property'sini bulur

    public InMemoryRepository()
    {
        // Reflection ile T tipinin mutlaka Id property’si olmalı, yoksa exception fırlat
        _idProperty = typeof(T).GetProperty("Id") 
                      ?? throw new InvalidOperationException("Type must have an Id property.");
    }

    public IEnumerable<T> GetAll() => _items; // Tüm öğeleri döndürür

    public T? GetById(int id)
    {
        // Id ile eşleşen item'ı bul
        return _items.FirstOrDefault(x =>
        {
            var val = _idProperty.GetValue(x);
            return val is int intVal && intVal == id;
        });
    }

    public void Add(T item)
    {
        // Yeni item'a otomatik artan Id ata ve listeye ekle
        _idProperty.SetValue(item, _nextId++);
        _items.Add(item);
    }

    public void Update(T item)
    {
        // Gelen item'ın Id'sini al
        var val = _idProperty.GetValue(item);
        if (val is not int id)
            throw new InvalidOperationException("Invalid Id value.");

        // Önce eski kaydı sil, sonra güncel halini ekle
        Delete(id);
        _items.Add(item);
    }

    public void Delete(int id)
    {
        // Id ile eşleşen kaydı bulup sil
        var existing = GetById(id);
        if (existing != null)
        {
            _items.Remove(existing);
        }
    }
}
