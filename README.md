# Short project description
The goal was to design the data layer for a social network, populate it with initial and imported data, 
and then produce formatted exports from the database. The main focus was on correct EF Core modeling, validation, duplicate detection, and serialization formatting.

---
# Full desciption of the project

### Social Network Database Application

**Entity Framework Core Regular Exam** - SoftUni Databases Advanced course, 30 November 2025

**✅ Exam Result: 100/100 points** 

## Project Overview

Complete social network data layer built with **Entity Framework Core** using **Code First** approach. Includes domain modeling, XML/JSON import/export with validation, duplicate detection, and precise serialization formatting.

## Key Features

- **Domain Models** (60 pts): 6 entities (`User`, `Conversation`, `UserConversation`, `Post`, `Friendship`, `Message`) with constraints, relationships, composite keys, navigation properties
- **Database Seeding**: 9 users, 20 friendships, 6 conversations, 26 user-conversations
- **Data Import** (20 pts):
  - XML: 56 validated messages from `messages.xml`
  - JSON: 27 validated posts from `posts.json`
- **Data Export** (20 pts):
  - XML: Users with friendship counts + posts (ordered by username/PostId)
  - JSON: Conversations with chronological messages (ordered by StartedAt/SentAt)

## Technologies

- .NET Core + Entity Framework Core (Code First)
- LINQ queries + XML/JSON serialization
- SQL Server + data annotations + custom validation

## Architecture
```
SocialNetwork
│
├── Data/
│   ├── SocialNetwork.Data/                  # Database access layer
│   │   ├── Models/                          # Entity models and relationships
│   │   └── SocialNetworkDbContext.cs        # Database context
│   │
├── DataProcessor/
│   ├── ImportDTOs
│   ├── ExportDTOs
│   ├── Deserializer.cs                      # Import logic for XML and JSON datasets
│   └── Serializer.cs                        # Export logic for XML and JSON reports
│
├── Datasets/                                # Input files provided for the exam
│   ├── messages.xml
│   └── posts.json
│
├── ImportResults/                           # Generated import result outputs
│
├── ExportResults/                           # Generated export result outputs
│
├── Utilities/                               # Helpers for XML 
│
├── StartUp/  
│
└── SocialNetwork.sln
```

---
*Repository contains complete source code, datasets, and test results.*
