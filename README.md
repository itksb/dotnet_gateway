# dotnet_gateway
Http Gateway with simple authorization based on YARP  .NET

## Requirements

- .NET Core 6

## Get Started

- clone this repository
- cd src
- cp Configurations/yarp-proxy.json Configurations/yarp-proxy.Development.json
- cp Configurations/yarp-proxy.json Configurations/yarp-proxy.Production.json
- dotnet restore
- dotnet run 


## Configuration example (yarp-proxy.Production.json)

```json

{
  "ReverseProxy": {
    "Routes": {

      "route1": {
        "ClusterId": "cluster1",
        "Match": {
          "Path": "/{**catch-all}"
        },
        "AuthorizationPolicy": "accessUrlPolicy",
        "Transforms": [
          {
            "RequestHeader": "Authorization",
            "Set": "YOUR-HEADER-VALUE-HERE"
          }
        ]
      },

      "student": {
        "ClusterId": "host2",
        "Match": {
          "Path": "/student/{**catch-all}"
        },
        "AuthorizationPolicy": "accessUrlPolicy",
        "Transforms": [
          {
            "RequestHeader": "Authorization",
            "Set": "YOUR-HEADER-VALUE-HERE"
          }
        ]
      }


    },
    "Clusters": {
      "cluster1": {
        "Destinations": {
          "destination1": {
            "Address": "http://10.10.11.137:8080/"
          }
        }
      },

      "host2": {
        "Destinations": {
          "destination1": {
            "Address": "http://10.10.11.137:8080/"
          }
        }
      }

    }
  }
}

```

## Configuration example (role-user-access.Production.json)

```json

{
  "RoleUserAccess": {
    "roles": [
      {
        "name": "administrators",
        "users": [
          {
            "name": "administrator1",
            "password": "password"
          },
          {
            "name": "administrator2",
            "password": "password"
          }
        ],
        "access_urls": [
          "/{**catch-all}"
        ]
      },
      {
        "name": "moderators",
        "users": [
          {
            "name": "moderator1",
            "password": "password"
          }
        ],
        "access_urls": [
          [
            "Path description here for convenience"
          ],
          "/kdz/f91d039d-22c9-4ffd-85ba-372e6f6768c7",
          [
            "КДЗ для публикации 04"
          ],
          "/kdz/d655dd56-db02-4efd-a28c-b9c2075eeec5",
          "/kdz/css/{**catch-all}",
          "/resources/graph.txt",
          "/resources/editor.txt",
          "/kdz/resources/graph.txt",
          "/js/{**catch-all}",
          "/kdz/resources/editor.txt",
          "/kdz-srv/kdz-redactor",
          "/kdz-srv/kdz-redactor/global-rating-literals",
          "/{Page=Home}",
          "/css/{**css}",
          "/student/css/{**css}",
          "/student/js/{**js}",
          "/student/styles/{**styles}",
          "/student/api/user/student",
          "/student/assets/{**assets}",
          "/student/api/student",
          "/student/select-kdz",
          "/student/api/kdz-process",
          [
            "КДЗ для публикации 03"
          ],
          "/student/api/kdz-process/start-kdz/f91d039d-22c9-4ffd-85ba-372e6f6768c7",
          "/kdz-srv/kdz-redactor/f91d039d-22c9-4ffd-85ba-372e6f6768c7",
          [
            "Публикация КДЗ"
          ],
          "/student/api/cache/f91d039d-22c9-4ffd-85ba-372e6f6768c7",
          "/kdz-srv/kdz-redactor/f91d039d-22c9-4ffd-85ba-372e6f6768c7/{**catch-all}",
          "/kdz-vizit-editor/codifiers-editor/f91d039d-22c9-4ffd-85ba-372e6f6768c7",
          "/kdz-vizit-editor/general-info-editor/f91d039d-22c9-4ffd-85ba-372e6f6768c7",
          "/kdz-srv/general-information-redactor/f91d039d-22c9-4ffd-85ba-372e6f6768c7",
          [
            "КДЗ для публикации 04"
          ],
          "/student/api/kdz-process/start-kdz/d655dd56-db02-4efd-a28c-b9c2075eeec5",
          [
            "Публикация КДЗ"
          ],
          "/student/api/cache/d655dd56-db02-4efd-a28c-b9c2075eeec5",
          "/kdz-srv/kdz-redactor/d655dd56-db02-4efd-a28c-b9c2075eeec5/{**catch-all}",
          "/kdz-vizit-editor/codifiers-editor/d655dd56-db02-4efd-a28c-b9c2075eeec5",
          "/kdz-vizit-editor/general-info-editor/d655dd56-db02-4efd-a28c-b9c2075eeec5",
          "/kdz-srv/general-information-redactor/d655dd56-db02-4efd-a28c-b9c2075eeec5",
          "/kdz-vizit-editor/visit-editor/{**catch-all}",
          "/kdz-vizit-editor/js/{**js}",
          "/kdz-vizit-editor/css/{**css}",
          "/kdz-srv/visit-redactor/{**catch-all}",
          "/kdz-vizit-editor/favicon.ico",
          "/kdz-srv/kdz-redactor/visit/{**catch-all}"
        ]
      },
      {
        "name": "editors",
        "users": [
          {
            "name": "editor1",
            "password": "password"
          }
        ],
        "access_urls": [
          [
            "КДЗ для публикации 05"
          ],
          "/kdz/d58b161f-05f2-4266-93e6-c79cd45190ee",
          [
            "КДЗ для публикации 06"
          ],
          "/kdz/852c654d-b1d9-4352-93c4-d3b5d59514ea",
          "/kdz/css/{**catch-all}",
          "/resources/graph.txt",
          "/resources/editor.txt",
          "/kdz/resources/graph.txt",
          "/js/{**catch-all}",
          "/kdz/resources/editor.txt",
          "/kdz-srv/kdz-redactor",
          "/kdz-srv/kdz-redactor/global-rating-literals",
          "/{Page=Home}",
          "/css/{**css}",
          "/student/css/{**css}",
          "/student/js/{**js}",
          "/student/styles/{**styles}",
          "/student/api/user/student",
          "/student/assets/{**assets}",
          "/student/api/student",
          "/student/select-kdz",
          "/student/api/kdz-process",
          [
            "КДЗ для публикации 05"
          ],
          "/student/api/kdz-process/start-kdz/d58b161f-05f2-4266-93e6-c79cd45190ee",
          "/kdz-srv/kdz-redactor/d58b161f-05f2-4266-93e6-c79cd45190ee",
          [
            "Публикация КДЗ"
          ],
          "/student/api/cache/d58b161f-05f2-4266-93e6-c79cd45190ee",
          "/kdz-srv/kdz-redactor/d58b161f-05f2-4266-93e6-c79cd45190ee/{**catch-all}",
          "/kdz-vizit-editor/codifiers-editor/d58b161f-05f2-4266-93e6-c79cd45190ee",
          "/kdz-vizit-editor/general-info-editor/d58b161f-05f2-4266-93e6-c79cd45190ee",
          "/kdz-srv/general-information-redactor/d58b161f-05f2-4266-93e6-c79cd45190ee",
          [
            "КДЗ для публикации 06"
          ],
          "/student/api/kdz-process/start-kdz/852c654d-b1d9-4352-93c4-d3b5d59514ea",
          [
            "Публикация КДЗ"
          ],
          "/student/api/cache/852c654d-b1d9-4352-93c4-d3b5d59514ea",
          "/kdz-srv/kdz-redactor/852c654d-b1d9-4352-93c4-d3b5d59514ea/{**catch-all}",
          "/kdz-vizit-editor/codifiers-editor/852c654d-b1d9-4352-93c4-d3b5d59514ea",
          "/kdz-vizit-editor/general-info-editor/852c654d-b1d9-4352-93c4-d3b5d59514ea",
          "/kdz-srv/general-information-redactor/852c654d-b1d9-4352-93c4-d3b5d59514ea",
          "/kdz-vizit-editor/visit-editor/{**catch-all}",
          "/kdz-vizit-editor/js/{**js}",
          "/kdz-vizit-editor/css/{**css}",
          "/kdz-srv/visit-redactor/{**catch-all}",
          "/kdz-vizit-editor/favicon.ico",
          "/kdz-srv/kdz-redactor/visit/{**catch-all}"
        ]
      }


    ]
  }
}

```
