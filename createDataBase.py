import sqlite3
import os

# Path to the SQLite database file
db_file = 'project.db'

# Check if the file exists
# if os.path.exists(db_file):
# Delete the file
# os.remove(db_file)
# print(f"{db_file} deleted successfully.")
# else:
# print(f"{db_file} does not exist.")

# Step 1: Connect to a database (or create it if it doesn't exist)
connection = sqlite3.connect(db_file)

# Step 2: Create a cursor object to execute SQL commands
cursor = connection.cursor()

# cursor.execute('''DROP TABLE Accounts''')

# Step 3: Create the Account table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Accounts (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    email TEXT NOT NULL,
    password TEXT NOT NULL,
    fullname TEXT,
    admin BOOLEAN NOT NULL DEFAULT false
);
''')

cursor.execute('''
INSERT OR IGNORE INTO Accounts (id, email, password, fullname, admin)
VALUES (?, ?, ?, ?, ?)
''', (0, "admin", "admin", "Admin", True))

# cursor.execute('''DROP TABLE IF EXISTS Reservations''')

# Step 4: Create the Reservation table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Reservations (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Account_ID INTEGER NOT NULL,
    Schedule_ID INTEGER NOT NULL,
    seat_Row INTEGER NOT NULL,
    seat_Collum INTEGER NOT NULL,           
    status TEXT NOT NULL DEFAULT Active,
    Location_ID INTEGER NOT NULL,
    FOREIGN KEY (Account_ID) REFERENCES Accounts(id)
    FOREIGN KEY (Schedule_ID) REFERENCES Schedule(id)
    FOREIGN KEY (Location_ID) REFERENCES Location(id)
);
''')

# cursor.execute('''DROP TABLE Auditorium''')

# Step 5: Create the Auditorium table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Auditorium (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    room INTEGER NOT NULL,
    type TEXT,
    total_seats INTEGER NOT NULL
);
''')

# cursor.execute('''DROP TABLE Seats''')

# Step 6: Create the Seats table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Seats (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Auditorium_ID INTEGER NOT NULL,
    row INTEGER NOT NULL,
    collum INTEGER NOT NULL,
    price FLOAT NOT NULL,
    type INTEGER NOT NULL,
    isAvailable BOOLEAN DEFAULT true,
    FOREIGN KEY (Auditorium_ID) REFERENCES Auditorium(id)
);
''')

# cursor.execute('''DROP TABLE Schedule''')

# Step 7: Create the Schedule table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Schedule (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    startTime DATETIME NOT NULL,
    endTime DATETIME NOT NULL,
    Movie_ID INTEGER NOT NULL,
    Auditorium_ID INTEGER NOT NULL,
    Location_ID INTEGER NOT NULL,
    FOREIGN KEY (Movie_ID) REFERENCES Movies(id)
    FOREIGN KEY (Auditorium_ID) REFERENCES Auditorium(id)
    FOREIGN KEY (Location_ID) REFERENCES Location(id)
);
''')

# cursor.execute('''DROP TABLE Movies''')

# Step 8: Create the Movie table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Movies (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    director TEXT NOT NULL,
    description TEXT NOT NULL,
    length TIME NOT NULL,
    genre TEXT NOT NULL,
    age_rating INTERGER,
    movie_ratings DECIMAL(2,1)
);
''')

# Step 9: Create the Auditorium_layout table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Auditorium_layout (
    room_id INTEGER,
    row_num INTEGER NOT NULL,
    col_num INTEGER NOT NULL,
    seat_class TINYINT NOT NULL,
    price DECIMAL(4,2) NOT NULL,
    PRIMARY KEY (room_id, row_num, col_num)
);
''')

# Step 10: Create the Schedule table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Bar_reservation (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    startTime DATETIME NOT NULL,
    endTime DATETIME NOT NULL,
    Account_ID INTEGER NOT NULL,
    Reservation_ID INTEGER NOT NULL,
    FOREIGN KEY (Account_ID) REFERENCES Accounts(id)
    FOREIGN KEY (Reservation_ID) REFERENCES Reservations(id)
);
''')

# Step 11: Create the Available Snacks table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Available_snacks (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    price FLOAT NOT NULL
);
''')

# Step 12: Create the Bought Snacks table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Bought_snacks (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Account_ID INTEGER NOT NULL,
    Reservation_ID INTEGER NOT NULL,
    Snack_ID INTEGER NOT NULL,
    amount INTERGER NOT NULL,
    FOREIGN KEY (Account_ID) REFERENCES Accounts(id)
    FOREIGN KEY (Reservation_ID) REFERENCES Reservations(id)
    FOREIGN KEY (Snack_ID) REFERENCES Available_snacks(id)
    
);
''')

# Step 13: Create the Location table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Location (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL
);
''')

# Step 14: Create the Assigned Roles table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Assigned_Roles (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Role INTEGER NOT NULL,
    Account_ID INTEGER NOT NULL,
    Location_ID INTEGER NOT NULL,
    FOREIGN KEY (Role) REFERENCES Roles(id)
    FOREIGN KEY (Account_ID) REFERENCES Accounts(id)
    FOREIGN KEY (Location_ID) REFERENCES Location(id)
);
''')

# Step 15: Create the Roles table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Roles (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    functionality TEXT NOT NULL,
    level_Needed INTEGER NOT NULL
);
''')

# Step 16: Commit changes and close the connection
connection.commit()
connection.close()

print("Database and tables created successfully.")
