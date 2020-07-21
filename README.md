# ConcurrentTwoWayMapping <img src="https://user-images.githubusercontent.com/6120604/88040301-5e1d7600-cb51-11ea-8665-8881b6d03183.png" align="right" width="128" height="128" />(https://github.com/imbialik/ConcurrentTwoWayMapping)
> Concurrent generic 2 way mapping between elments



## Examples

  ### usage

```csharp
    var concurrentTwoWayMapping = new ConcurrentTwoWayMapping<Guid, int>();
    var guid = Guid.NewGuid();
    concurrentTwoWayMapping.TryAdd(guid, 2);
    concurrentTwoWayMapping.TryAdd(guid, 5);
    if (concurrentTwoWayMapping.TryGetValue(guid, out var integersEnumerable))
    {
        // integersEnumerable will contains 2,5
    }

    var guid2 = Guid.NewGuid();
    concurrentTwoWayMapping.TryAdd(guid2, 2);

    if (concurrentTwoWayMapping.TryGetValue(2, out var guidEnumerable))
    {
        // guidEnumerable will contains guid & guid2
    }
```
    
  ### same type usage
```csharp
    var sameTypeConcurrentTwoWayMapping = new ConcurrentTwoWayMapping<int, int>();
    int key1 = 2, value1 = 2,value2 = 5;
    sameTypeConcurrentTwoWayMapping.TryAdd(key1, value1);
    sameTypeConcurrentTwoWayMapping.TryAdd(key1, value2);
    if (sameTypeConcurrentTwoWayMapping.TryGetValueByFirstKey(key1, out var integersEnumerable))
    {
        // integersEnumerable will contains value1,value2
    }

    if (sameTypeConcurrentTwoWayMapping.TryGetValueBySecondKey(value1, out var integersEnumerable2))
    {
        //integersEnumerable2 will contains key1
    }
```
