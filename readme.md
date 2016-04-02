# Trinity #
This solution provides `ASP.NET Core` Respository classes for [Neo4jClient](https://github.com/Readify/Neo4jClient).

Documentation is light, but it is used in several production applications via submodules (no nuget yet) 

* `NeoRepository` is a base respository from which to inherit concrete repositories.

* `AppSettingRepository` maintains nodes for storing string settings and incrementing counters. See [IAppSettingRepository](https://github.com/neutmute/trinity/blob/master/src/Trinity/Repositories/IAppSettingRepository.cs) for more hints as to its uses

Your repositories would be registered using IoC and the `config.json` would look something like this
    
      {
    	"Data": {
      		"Neo4j": {
    			"GraphUri": "http://localhost:7482/db/data"
    			,"IsCypherLoggingEnabled": false
        	}
    	}
      }




