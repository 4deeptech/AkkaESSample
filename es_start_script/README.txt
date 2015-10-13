The start-ex.bat file is the command line used to fire up EventStore using basic defaults.  

On windows this must be run as admin and the following two folders should be created under the folder where you unzip Event Store runtime files:

esdata
eslogs

Command line passing in the folders where the data and logs should go


EventStore.ClusterNode.exe --db ./esdata --log ./eslogs --run-projections=all