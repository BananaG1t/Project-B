import sqlite3

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

cursor.execute('''DROP TABLE IF EXISTS Accounts''')

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
''', (0, "A1", "AP1", "Admin", True))

cursor.execute('''
INSERT OR IGNORE INTO Accounts (id, email, password, fullname, admin)
VALUES (?, ?, ?, ?, ?)
''', (1, "U1", "UP1", "User", False))

cursor.execute('''DROP TABLE IF EXISTS SeatReservations''')
cursor.execute('''DROP TABLE IF EXISTS Reservations''')
cursor.execute('''DROP TABLE IF EXISTS BarReservation''')

# Step 4: Create the SeatReservation table
cursor.execute('''
CREATE TABLE IF NOT EXISTS SeatReservations (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Order_ID INTERGER NOT NULL,
    seat_Row INTEGER NOT NULL,
    seat_Collum INTEGER NOT NULL,           
    status TEXT NOT NULL DEFAULT Active,
    FOREIGN KEY (Order_ID) REFERENCES Orders(id)
);
''')

cursor.execute('''DROP TABLE IF EXISTS Auditorium''')

# Step 5: Create the Auditorium table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Auditorium (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    room INTEGER NOT NULL,
    type TEXT,
    total_seats INTEGER NOT NULL
);
''')

cursor.execute('''DROP TABLE IF EXISTS Seats''')

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

cursor.execute('''DROP TABLE IF EXISTS Schedule''')

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

# cursor.execute('''DROP TABLE IF EXISTS Movies''')

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

# Step 9: Create the AuditoriumLayout table
cursor.execute('''
CREATE TABLE IF NOT EXISTS AuditoriumLayout (
    room_id INTEGER,
    row_num INTEGER NOT NULL,
    col_num INTEGER NOT NULL,
    seat_class TINYINT NOT NULL,
    price DECIMAL(4,2) NOT NULL,
    PRIMARY KEY (room_id, row_num, col_num)
);
''')

cursor.execute('''DROP TABLE IF EXISTS Orders''')

# Step 10: Create the Orders table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Orders (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Account_ID INTEGER NOT NULL,
    Schedule_ID INTEGER NOT NULL,
    amount INTERGET NOT NULL,
    bar BOOLEAN NOT NULL,
    FOREIGN KEY (Account_ID) REFERENCES Accounts(id)
    FOREIGN KEY (Schedule_ID) REFERENCES Schedule(id)
);
''')

cursor.execute('''DROP TABLE IF EXISTS Snacks''')
cursor.execute('''DROP TABLE IF EXISTS AvailableSnacks''')

# Step 11: Create the Available Snacks table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Snacks (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    price FLOAT NOT NULL
);
''')

cursor.execute('''DROP TABLE IF EXISTS BoughtSnacks''')

# Step 12: Create the Bought BoughtSnacks table
cursor.execute('''
CREATE TABLE IF NOT EXISTS BoughtSnacks (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Reservation_ID INTEGER NOT NULL,
    Snack_ID INTEGER NOT NULL,
    amount INTERGER NOT NULL,
    FOREIGN KEY (Reservation_ID) REFERENCES SeatReservations(id)
    FOREIGN KEY (Snack_ID) REFERENCES Snacks(id)
    
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
CREATE TABLE IF NOT EXISTS AssignedRoles (
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
    name TEXT NOT NULL,
    level_Access INTEGER NOT NULL
);
''')

# Step 16: Create the Role Level table
cursor.execute('''
CREATE TABLE IF NOT EXISTS RoleLevel (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    functionality TEXT NOT NULL,
    level_Needed INTEGER NOT NULL
);
''')

cursor.execute('''DROP TABLE IF EXISTS Coupons''')
# Step 17: Create the Coupons table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Coupons (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    coupon_code INTEGER NOT NULL,
    expiration_date DATETIME NOT NULL,
    coupon_type TEXT NOT NULL,
    coupon_percentage BOOLEAN NOT NULL DEFAULT false,
    amount INTEGER NOT NULL,
    account_ID INTEGER NOT NULL,
    FOREIGN KEY (Account_ID) REFERENCES Accounts(id)
);
''')

# Step 18: Commit changes and close the connection
connection.commit()
connection.close()

print("Database and tables created successfully.")
