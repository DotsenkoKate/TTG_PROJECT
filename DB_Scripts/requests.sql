insert into way(number, price, owner_id) values(75, 25, 1);

update auto set status = 'Требуется ремонт' where number = 'М002МА134rus';

select number, brand, model, capacity, release_date from auto where driver_id = 2;

SELECT way_waystation.way_number, name, place FROM way
JOIN way_waystation ON way.number=way_waystation.way_number
JOIN waystation ON way_waystation.waystation_id=waystation.id
WHERE way_waystation.way_number=1;

SELECT name, number FROM owner
JOIN way ON owner.id=way.owner_id
WHERE name='Иванов Кирилл Михайлович';