from pymongo import MongoClient
from sqlalchemy import *

server_uri = "mongodb+srv://cyber_essence:Tbz9uv5m6fS2HEc@cluster0.ha8y8.mongodb.net/myFirstDatabase?retryWrites=true&w=majority"
client = MongoClient(server_uri)

db = client.dbms 
users = db.ttg 
owners = db.ttg
trip = db.ttg
divide = db.ttg

def input_user():
    user_db = []
    cat_id = int(input('Введите id категории: '))
    user_db.append(cat_id)
    login = input('Введите логин: ')
    user_db.append(login)
    password = input('Введите пароль: ')
    user_db.append(password)
    name = input('Введите имя: ')
    user_db.append(name)
    return user_db
    

def input_owner():
    owner_db = []
    id = int(input('Введите id: '))
    owner_db.append(id)
    source_type = input('Введите тип источника: ')
    owner_db.append(source_type)
    text = input('Введите текст: ')
    owner_db.append(text)
    return owner_db

def input_trip():
    trip_db = []
    id = int(input('Введите id: '))
    trip_db.append(id)
    way_number = input('Введите номер пути: ')
    trip_db.append(way_number)
    date = input('Введите дату: ')
    trip_db.append(date)
    auto_number = input('Введите номер автомобиля: ')
    trip_db.append(auto_number)
    start = input('Введите стартовую дату путешествия: ')
    trip_db.append(start)
    finish = input('Введите конечную дату путешествия: ')
    trip_db.append(finish)
    return trip_db

def input_divide_of_trip():
    divide_db = []
    id = int(input('Введите id: '))
    divide_db.append(id)
    trip_id = int(input('Введите id путешествия: '))
    divide_db.append(trip_id)
    station_id = int(input('Введите id станции: '))
    divide_db.append(station_id)
    time = input('Введите время: ')
    divide_db.append(time)
    return divide_db



def insert_fields_user():
    num_of_records = int(input('Введите кол-во вставляемых записей: '))
    for i in range(num_of_records):
        db.users.insert_one({"cat_id": input_user()[0], "login": input_user()[1], "password": input_user()[2], "name": input_user()[3]})

def insert_fields_owner():
    num_of_records = int(input('Введите кол-во вставляемых записей: '))
    for i in range(num_of_records):
        db.owners.insert_one({"id": input_owner()[0], "source_type": input_owner()[1], "text": input_owner()[2]})

def insert_fields_trip():
    num_of_records = int(input('Введите кол-во вставляемых записей: '))
    for i in range(num_of_records):
        db.trip.insert_one({"id": input_trip()[0], "way_number": input_trip()[1], "date": input_trip()[2], "auto_number": input_trip()[3], "start": input_trip()[4], "finish": input_trip()[5]})

def insert_fields_divide():
    num_of_records = int(input('Введите кол-во вставляемых записей: '))
    for i in range(num_of_records):
        db.divide.insert_one({"id": input_divide_of_trip()[0], "trip_id": input_divide_of_trip()[1], "station_id": input_divide_of_trip()[2], "time": input_divide_of_trip()[3]})

#аналитический запрос - добавить юзера
inp = int(input('''Введите 1, чтобы вставить записи в таблицу users\nВведите 2, чтобы вставить записи в таблицу owners\nВведите 3, чтобы вставить записи в таблицу trip\nВведите 4, чтобы вставить записи в таблицу divide_of_trip\n '''))

if inp == 1:
    insert_fields_user()
elif inp == 2:
    insert_fields_owner()
elif inp == 3:
    insert_fields_trip()
elif inp == 4:
    insert_fields_divide()
else:
    print('Ошибка')

'''query = db.col.find({"login": "gate"})

for doc in query:
    print(doc)

#db.col.remove({"login" : "pog"})

#db.col.update_one({"login": "silverhand"}, {"$set": {"login": "gate"}})
db.col.update_one({"login": "gate"}, {"$set": {"login": "silverhand"}})'''


