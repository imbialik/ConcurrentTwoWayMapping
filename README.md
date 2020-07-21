# ConcurrentTwoWayMapping [![Awesome](https://user-images.githubusercontent.com/6120604/88038411-d9315d00-cb4e-11ea-8b2b-bb7f2d6164f8.png)](https://github.com/imbialik/ConcurrentTwoWayMapping)
> Concurrent generic 2 way mapping between elments

<img src="https://user-images.githubusercontent.com/6120604/88038922-90c66f00-cb4f-11ea-8d1f-5313c6ea5029.png" align="right" />

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
