import random
import datetime as dt
import csv
plane_models = ['NG Model 15001', 'NG Model 20103', 'Panda 202015', 'Panda 202208', 'NG Model 22009', 'Herpa 515726']
flying_number = ["1000", "2000", "3000", "4000", "5000", "6000", "7000", "8000", "9000"]

def planes():
    load_capacity = ['2.7', '5.0', '3.5', '4.0', '10.0', '12.0', '4.5']
    efficiencies = ['3,34', '3,29', '3,67', '9,49']
    max_passenger = ['165', '170', '180', '190', '200', '210']


    with open ('planes.csv', 'w') as csvfile:
        writer = csv.writer(csvfile, delimiter = ';', quotechar='|', quoting=csv.QUOTE_MINIMAL)
        writer.writerow(['Model', 'LoadCapacity', 'Efficinty', 'PassengerMax'])
        
        arr = ['0']*4
        for model in plane_models:
            arr[0] = '\"' + model + '\"'
            arr[1] = '\"' + load_capacity[random.randint(0, len(load_capacity) - 1)] + '\"'
            arr[2] = '\"' + efficiencies[random.randint(0, len(efficiencies) - 1)] + '\"'
            arr[3] = '\"' + max_passenger[random.randint(0, len(max_passenger) - 1)] + '\"'
            
            print(arr)
            writer.writerow(arr)
        
def passenger():  
    FIOs = ['Zamotohina Maria Andreevna', 'Nenashev Ivan Nickolayevich', 
        'Chumackov Michail Vladimirovich', 'Zemlyanov Vladimir Borisovich', 'Belyackova Veronicka Sergeevna', 'Zhuckov Semyon Viktorovich']
    passport = ['3678678903', '1234567890', '5678359284', '7492748492', '837968585', '2875909090']
    pass_info = {}
    pass_flies = {}
    for f in FIOs:
        x = random.randint(0, len(passport) - 1)
        pass_info[f] = passport[x]
        passport.pop(x)
        pass_flies[f] = []
        
    seats = ['A', 'B', 'C', 'D', 'E', 'F']
    

    with open ('passengers.csv', 'w') as csvfile:
        writer = csv.writer(csvfile, delimiter = ';', quotechar='|', quoting=csv.QUOTE_MINIMAL)
        writer.writerow(['FIO', 'Passport', 'Registration', 'Ticketnumber', 'SeatNumber', 'BaggageWeight', 'CodeFlight'])
        arr = ['0']*7
        
        for i in range(20):
            fio = FIOs[random.randint(0, len(FIOs) - 1)]
            arr[0] = '\"' + fio + '\"'
            arr[1] = '\"' + pass_info[fio] + '\"'
            arr[2] = "\"true\""
            arr[3] = '\"' + f"{random.randint(10000, 100000)}" + '\"'
            arr[4] = '\"' + f"{seats[random.randint(0, len(seats) - 1)]}" + f"{random.randint(1, 20)}" + '\"'
            arr[5] = '\"' + f"{random.randint(10, 100)/10}" + '\"'
            x = random.randint(0, len(flying_number) - 1)
            while flying_number[x] in pass_flies.get(fio):
                if len(pass_flies.get(fio)) == len(flying_number): break
                x = random.randint(0, len(flying_number) - 1)
                
            arr[6] = '\"' + flying_number[x] + '\"'
            pass_flies[fio].append(flying_number[x])
            writer.writerow(arr)
            
def airflying():  
    deparure_points = ['Tokio', 'Moscow', 'Samara', 'Rome', 'London', 'Los-Angeles']
    arrive_points = ['St. Petersburg', 'Washington', 'Dublin', 'Amsterdam', 'Vienna']
    

    with open ('airflyights.csv', 'w') as csvfile:
        writer = csv.writer(csvfile, delimiter = ';', quotechar='|', quoting=csv.QUOTE_MINIMAL)
        writer.writerow(['CodeNumber', 'DeparturePoint', 'ArrivalPoint', 'Departure', 'Arrive', 'FlyingTime', 'PlaneType'])
        arr = ['0']*7
        
        for f in flying_number:
            arr[0] = '\"' + f + '\"'
            arr[1] = '\"' + deparure_points[random.randint(0, len(deparure_points) - 1)] + '\"'
            arr[2] = '\"' + arrive_points[random.randint(0, len(arrive_points) - 1)] + '\"'
            
            depar = dt.datetime(year=2024, month=random.randint(1, 12), day=random.randint(1, 29), 
                                hour=random.randint(0, 23), minute=random.randint(0, 59))
            arrive = depar + dt.timedelta(hours=random.randint(1, 12), minutes=random.randint(1, 59))
            arr[3] = '\"' + f"{depar}" + '\"'
            arr[4] = '\"' + f"{arrive}" + '\"'
            arr[5] = '\"' + f"{arrive - depar}" + '\"'
            arr[6] = '\"' + plane_models[random.randint(0, len(plane_models) - 1)] + '\"'
            
            writer.writerow(arr)
#planes()    
passenger()
#airflying()