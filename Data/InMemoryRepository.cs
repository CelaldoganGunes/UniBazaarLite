using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class InMemoryRepository<T> : IRepository<T> where T : class
{
    private readonly List<T> _items = new();
    private int _nextId = 1;

    private readonly PropertyInfo _idProperty;

    public InMemoryRepository()
    {
        _idProperty = typeof(T).GetProperty("Id") 
                      ?? throw new InvalidOperationException("Type must have an Id property.");
    }

    public IEnumerable<T> GetAll() => _items;

    public T? GetById(int id)
    {
        return _items.FirstOrDefault(x =>
        {
            var val = _idProperty.GetValue(x);
            return val is int intVal && intVal == id;
        });
    }

    public void Add(T item)
    {
        _idProperty.SetValue(item, _nextId++);
        _items.Add(item);
    }

    public void Update(T item)
    {
        var val = _idProperty.GetValue(item);
        if (val is not int id)
            throw new InvalidOperationException("Invalid Id value.");

        Delete(id);
        _items.Add(item);
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing != null)
        {
            _items.Remove(existing);
        }
    }
}
