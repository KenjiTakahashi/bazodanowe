var Faker = require('Faker');
var fs = require('fs');
var crypto = require('crypto');

if(process.argv.length != 4) {
    console.log("Incorrect number of arguments");
    process.exit(1);
}

var clients = parseInt(process.argv[2]);
var workers = parseInt(process.argv[3]);
if(isNaN(clients) || isNaN(workers)) {
    console.log("Arguments must be numbers");
    process.exit(1);
}

var roles = ["Owner", "Manager", "Programmer", "Accounter", "Mechanic"];

function randomDate() {
    var start = new Date(1970, 0, 1);
    var end = new Date();
    var d = new Date(
        start.getTime() + Math.random() * (
            end.getTime() - start.getTime()
        )
    );
    return (d.getMonth() + 1) + '-' + d.getDate() + '-' + d.getFullYear();
}

function biggerDate (date) {
    var d = new Date(date);
    d.setDate(d.getDate() + Faker.random.number(300));
    return (d.getMonth() + 1) + '-' + d.getDate() + '-' + d.getFullYear();
}

function nextDay(day, offset) {
    var d = new Date(day);
    d.setDate(d.getDate() + offset);
    return (d.getMonth() + 1) + '-' + d.getDate() + '-' + d.getFullYear();
}

var pesels = [];
function pesel() {
    var p;
    do {
        var s = '';
        for(var i = 0; i < 11; i++) {
            s += Faker.random.number(10);
        }
        while(s[0] === '0') {
            s = s.replace('0', Faker.random.number(10));
        }
        p = parseInt(s);
        if(pesels.indexOf(p) === -1) {
            pesels.push(p);
            break;
        }
    } while(1);
    return p;
}

function randomstring(L) {
    var s = '';
    var randomchar = function() {
        var n = Math.floor(Math.random() * 35)  + 10;
        return String.fromCharCode(n + 55);
    }
    while(s.length < L) {
        s += randomchar();
    }
    return s;
}

function nrrej() {
    var str = randomstring(3) + '-';
    for(var i = 0; i < 4; i++) {
        str += Faker.random.number(10).toString();
    }
    return str;
}

var numberOfCars = 0;

var str = "declare @id int\nset quoted_identifier off\n";
for(var i = 0; i < workers; i++) {
    str += "insert into Pracownik";
    str += " (Imie, Nazwisko, Rola, Login, Haslo) values (";
    str += '"' + Faker.Name.firstName() + '", ';
    str += '"' + Faker.Name.lastName() + '", ';
    str += '"' + Faker.random.number(4) + '", ';
    str += '"' + Faker.Internet.userName() + '", ';
    var hash = crypto.createHash('sha1');
    hash.update(Faker.Company.catchPhrase());
    str += '"' + hash.digest('hex') + '")\n';
    str += "set @id = (select TOP 1 ID from Pracownik";
    str += " order by ID desc)\n";
    if(!Faker.random.number(8)) {
        str += "insert into Urlop";
        str += " (ID_Pracownik, data_rozpoczecia, data_zakonczenia) values (";
        str += '@id, ';
        var kd = randomDate();
        var nd = biggerDate(kd);
        str += '"' + kd + '"' + ', "' + nd + '")\n';
    }
}
for(var j = 0; j < 10; j++) {
    str += "insert into TekstySms";
    str += " (Tresc) values (";
    str += '"' + Faker.Company.catchPhrase() + '")\n';
}
for(var i = 0; i < clients; i++) {
    str += "insert into Klient";
    str += " (PESEL, Imie, Nazwisko, DataRejestracji, email, Telefon) values (";
    str += pesel() + ', ';
    str += '"' + Faker.Name.firstName() + '", ';
    str += '"' + Faker.Name.lastName() + '", ';
    var kd = randomDate();
    str += '"' + kd + '", ';
    str += '"' + Faker.Internet.email() + '", ';
    str += '"' + Faker.PhoneNumber.phoneNumber() + '")\n';
    str += "set @id = (select TOP 1 ID from Klient";
    str += " order by ID desc)\n";
    for(var j = 0; j < Faker.random.number(3); j++) {
        str += "insert into Pojazd";
        str += " (ID_Klient, NrRej, DataPierwszejRejestracji) values (";
        str += '@id, ';
        str += '"' + nrrej() + '", ';
        var cd;
        do {
            cd = randomDate();
        } while(kd < cd);
        str += '"' + cd + '")\n';
        numberOfCars += 1;
    }

}
for(var i = 0; i < numberOfCars; i++) {
    str += "insert into Przeglad";
    str += " (ID_Pojazd, DataPlanowana, DataNastepnego";
    str += ", ID_Przyjmujacego, ID_Wykonujacego) values (";
    str += '(select TOP 1 ID from Pojazd order by newid()), ';
    var cd = randomDate();
    var kd = biggerDate(cd);
    str += '"' + kd + '", ';
    str += '"' + cd + '", ';
    str += '(select TOP 1 ID from Pracownik order by newid()), ';
    str += '(select TOP 1 ID from Pracownik order by newid()))\n';
    for(var k = 0; k < 4; k++) {
        str += "insert into Sms";
        str += " (ID_TekstySms, ID_Pojazd, DataWyslania, ";
        str += "DataNastepnego) values (";
        str += '(select TOP 1 ID from TekstySms order by newid()), ';
        str += '(select TOP 1 ID from Pojazd order by newid()), ';
        var kd = randomDate();
        str += '"' + kd + '", ';
        var cd = biggerDate(kd);
        str += '"' + cd + '")\n';
    }
}

var fd = fs.openSync(__dirname + '\\data.sql', 'w');
fs.writeSync(fd, str);
fs.closeSync(fd);