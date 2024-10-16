using System;
using System.Collections.Generic;
using NUnit.Framework;

public class ZipTests
{
    private List<Girl> girls;
    private List<Key>  keys;
    

    [SetUp]
    public void SetUp(){
        girls = new List<Girl>{
            new Girl(){ Name = "Mary" },
            new Girl(){ Name = "Lisa" },
            new Girl(){ Name = "Judi" },
            new Girl(){ Name = "Winnie" },
            new Girl(){ Name = "Mofi" }
        };

        keys = new List<Key>{
            new Key(){ CarType = CarType.Toyota, Name = "John" },
            new Key(){ CarType = CarType.Ford, Name = "Paul" },
            new Key(){ CarType = CarType.Volvo, Name = "Bob" }
        };
    }

    [Test]
    [Category("Refactoring/TDD")]
    public void GirlsAndKeysDoPairs(){

        var actual   = girls.Pairs(keys, (girl, key) => $"{girl.Name}-{key.Name}");
        var expected = new List<string>{
            "Mary-John",
            "Lisa-Paul",
            "Judi-Bob"
        };

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [Category("Refactoring/TDD")]
    public void KeysAndGirlsDoPairs(){

        var actual   = girls.Pairs(keys, (girl, key) => $"{key.Name}-{girl.Name}");
        var expected = new List<string>{
            "John-Mary",
            "Paul-Lisa",
            "Bob-Judi"
        };

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [Category("Refactoring/TDD")]
    public void NamesAndTicketNumbersDoPairs(){
        var names         = new[]{ "Joey", "David", "Tom" };
        var ticketNumbers = new[]{ 4, 5, 6, 7, 8 };
        
        var actual   = names.Pairs(ticketNumbers, (name, ticketNumber) => new Ticket{ CustomerName = name, TicketNumber = ticketNumber });
        var expected = new[]{
            new Ticket{ CustomerName = "Joey", TicketNumber = 4 },
            new Ticket{ CustomerName = "David", TicketNumber = 5 },
            new Ticket{ CustomerName = "Tom", TicketNumber = 6 },
        };

        Assert.AreEqual(expected, actual);
    }
}

public static class MyExtension
{
    public static IEnumerable<TResult> Pairs<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
                                                                       IEnumerable<TSecond> second,
                                                                       Func<TFirst, TSecond, TResult> Selector)
    {
        var firstEnumertor  = first.GetEnumerator();
        var secondEnumertor = second.GetEnumerator();

        TFirst  currentFirst;
        TSecond currentSecond;
        while(firstEnumertor.MoveNext() && secondEnumertor.MoveNext()){
            currentFirst  = firstEnumertor.Current;
            currentSecond = secondEnumertor.Current;
            yield return Selector(currentFirst, currentSecond);
        }
    }
}

public class Ticket : IEquatable<Ticket>
{
    public string CustomerName { get; set; }
    public int    TicketNumber { get; set; }

    public override int GetHashCode(){
        return CustomerName.GetHashCode() ^ TicketNumber.GetHashCode();
    }

    public bool Equals(Ticket other){
        return other != null && GetHashCode() == other.GetHashCode();
    }
}

public enum CarType
{
    Toyota,
    Ford,
    Volvo
}

public class Key
{
    public CarType CarType { get; set; }
    public string  Name { get; set; }
}

public class Girl
{
    public string Name { get; set; }
}