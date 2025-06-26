using System;
using System.ComponentModel.DataAnnotations;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}
