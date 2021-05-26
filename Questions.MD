# Questions and Comments

This document goes through some questions and thoughts on improvements that could be done if given more time

1. There should be much more testing done in order to make sure that the intervals are correct.
2. The storage module interface should be implemented by other storage solutions such as a db, key value store or maybe just an external file in JSON or similar.
3. Third party dependency for checking for holidays, Nager.Date, is not tested
4. Error handling could be improved with better error messages to the client
5. Logging could be enabled.
6. Some more optimzations could be done for the usage of the storage module. As of now, stored values are read on every request, an improvement could be to cache it.