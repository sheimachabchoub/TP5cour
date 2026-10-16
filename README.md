# TP5 - API Web ASP.NET Core

Ce dépôt est dédié au TP5 du cours d'API Web.

## État initial

- Solution : `TP5.slnx`
- Projet : `TP5.Api`
- Framework cible demandé par le cours : `.NET 8.0`
- Branche principale : `main`

Le cours `Chapitre5_APIWEB.pdf` est intégré dans le projet API : modèles, contexte Entity Framework, repositories, contrôleurs API, validation, recherche et migration initiale. La branche d'atelier sera complétée avec l'énoncé de l'atelier 5.

## Routes principales

- `GET /api/Employees`
- `GET /api/Employees/{id}`
- `GET /api/Employees/search?name=...`
- `POST /api/Employees`
- `PUT /api/Employees/{id}`
- `DELETE /api/Employees/{id}`
- `GET /api/Departments`
- `GET /api/Departments/{id}`

## Séparation de l'atelier Movies API

La branche `main` contient uniquement le projet du cours. La branche `tp5_atelier_seance2` contient uniquement l'atelier Movies, dans un projet séparé :

- Solution : `TP5-Atelier5.slnx` ;
- Projet : `TP5.Atelier5` ;
- Base de données : `MoviesAtelier5`.

L'atelier contient :

- `Genre` et `Movie` avec relation Entity Framework ;
- services `GenresService` et `MoviesService` ;
- DTO et mapping AutoMapper ;
- upload d'affiche `.png` ou `.jpg` de 1 Mo maximum ;
- endpoints CRUD `api/Genres` et `api/Movies` ;
- filtre `GET /api/Movies?genreId=...` ;
- CORS ouvert pour les clients de l'API.
