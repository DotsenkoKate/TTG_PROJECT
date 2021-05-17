SELECT text FROM Users
JOIN users_standart_message ON users_standart_message.user_id = Users.id
JOIN StandartMessage ON StandartMessage.id = users_standart_message.standart_message_id
WHERE Users.login = "andrey";

SELECT SavedWays.way_number FROM SavedWays 
JOIN users_ways ON users_ways.way_number = SavedWays.way_number
JOIN Users ON Users.id = users_ways.user_id
WHERE Users.login = "Andrey";

SELECT password FROM Users WHERE login = "andrey";

SELECT login FROM Users;