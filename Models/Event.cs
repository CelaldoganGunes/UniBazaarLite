using System;
using System.ComponentModel.DataAnnotations;

public class Event
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}
