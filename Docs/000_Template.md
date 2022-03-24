# 1. Logging events

Date: 2021-10-14

## Status

Accepted

## Context

In order to trace and track system events and behaviour, we need a mechanism to record the state along process flow.

## Decision

Therefore, we use log messages to catch events.

## Consequences

Every class, should implement a logger and log their behaviour. To be able to follow the data flow, we require a clear log message upon entry point containing
relevant information, when an object state has been changed and before returning the result.

### Guideline

The log message should contains class and method name, including parameters if available.

* **INFO:** Domain specific information, does not contain technical information.
* **DEBUG:** Information about CUD changes of objects
* **ERROR:** When exceptions are thrown
* **TRACE:** Receiving objects or do void method calls

#### Example

```java
import java.util.UUID;

public class Foo {

    private static final Logger logger = LogManager.getLogger(MethodHandles.lookup().lookupClass());

    public Bar bar(UUID barId, int count) {
        try {
            // Fetch Bar and create something
            logger.debug("Foo.bar(barId='{}', count={}) entry [{}] created.", id, count, something);
            // Return bar
        } catch (Exception e) {
            logger.error("Foo.bar(barId='{}', count={}), error [{}] occurred.", id, count, e.getMessage());
            // Block of code to handle errors
        }
        // code emitted
    }
```
