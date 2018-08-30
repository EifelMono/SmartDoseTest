# Test External Order mit Medizin Abfrage im REST interface

## Optionaler Parameter beim Order anlegen "*CheckMedicine=true*"

```
[AcceptVerbs("POST")]
[Route("Orders/")]
public HttpResponseMessage Post([FromBody] RestExternalOrder restexternalOrder, bool CheckMedicine = false)
```

* keine Medizinprüfung

  * http://x.x.x.x:6040/SmartDose/Orders

* mit MedizinPrüfung

  * http://x.x.x.x:6040/SmartDose/Orders?CheckMedicine=true

# SQLite und leerer Datenbank

| Medicine Count | Time     | Time per Element  |
|:--------------:|:--------:|:-----------------:|
|              1 |  2563 ms |              2563 |
|              2 |   203 ms |               101 |
|              5 |   348 ms |                69 |
|             10 |   654 ms |                65 |
|             50 |  2971 ms |                59 |
|            100 |  6069 ms |                60 |
|           1000 | 60273 ms |                60 |
|            100 |  6083 ms |                60 |
|             50 |  3130 ms |                62 |
|             10 |   680 ms |                68 |
|              5 |   347 ms |                69 |
|              2 |   176 ms |                88 |
|              1 |   109 ms |               109 |


# MSSQL 10.240.140.69 SmartDoseTrunk 42347 Medizineinträge

| Medicine Count | Time     | Time per Element  |
|:--------------:|:--------:|:-----------------:|
|              1 |     1412 |              1412 |
|              2 |      416 |               208 |
|              5 |      556 |               111 |
|             10 |      314 |                31 |
|             50 |     1414 |                28 |
|            100 |     2986 |                29 |
|           1000 |    28754 |                28 |
|            100 |     3635 |                36 |
|             50 |     1418 |                28 |
|             10 |      336 |                33 |
|              5 |      137 |                27 |
|              2 |       64 |                32 |
|              1 |       31 |                31 |

GetMedicines mit DeSerialisierung
<hr>
Medicines=42347

GetMedicines StatusCode=OK [200]

Elapsed time=00:00:09.8213863

# MSSQL 127.0.0.1 SmartDoseAKTest 42347 Medizineinträge

| Medicine Count | Time     | Time per Element  |
|:--------------:|:--------:|:-----------------:|
|              1 |  2508 ms |              2508 |
|              2 |   205 ms |               102 |
|              5 |   409 ms |                81 |
|             10 |   756 ms |                75 |
|             50 |  3482 ms |                69 |
|            100 |  6487 ms |                64 |
|           1000 | 61980 ms |                61 |
|            100 |  6228 ms |                62 |
|             50 |  3192 ms |                63 |
|             10 |   702 ms |                70 |
|              5 |   413 ms |                82 |
|              2 |   220 ms |               110 |
|              1 |   122 ms |               122 |


GetMedicines mit DeSerialisierung
<hr>
Medicines=43144

GetMedicines StatusCode=OK [200]

Elapsed time=00:00:25.1370855
