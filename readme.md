# Trinity #
This solution provides `ASP.NET Core` base respository and helper classes for [Neo4jClient](https://github.com/Readify/Neo4jClient).

Documentation is light, but it is used in several production applications via submodules (no nuget yet) 

* `NeoRepository` is a base respository from which to inherit concrete repositories.

* `AppSettingRepository` maintains nodes for storing string settings and incrementing counters. See [IAppSettingRepository](https://github.com/neutmute/trinity/blob/master/src/Trinity/Repositories/IAppSettingRepository.cs) for more hints as to its uses

* `IntegrityCheckRepository` and related classes are used to run sanity checks against the graph database. The power of graphs are relationships can be made anywhere, which is also a downfall if your repositories are not thoroughly tested. Use the power of the IntegrityCheckRepository.cs to go all cowboy and worry about it in retrospect! The integrity check repo helps you detect nodes that have gone rogue. Usage is via inheriting via `RawCheckRequest` and `RelationshipCheckRequest` and passing the results to the repository. Check the `Violations` property in the response and berate yourself accordingly.

#### Repository Usage
Your repositories would be registered using IoC and the `config.json` would look something like this
    
      {
    	"Data": {
      		"Neo4j": {
    			"GraphUri": "http://localhost:7482/db/data"
    			,"IsCypherLoggingEnabled": false
        	}
    	}
      }




