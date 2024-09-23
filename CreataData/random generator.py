import random
import csv

def planes():
    plane_models = ['NG Model 15001', 'NG Model 20103', 'Panda 202015', 'Panda 202208', 'NG Model 22009', 'Herpa 515726']
    load_capacity = ['2.7', '5.0', '3.5', '4.0', '10.0', '12.0', '4.5']
    efficiencies = ['3,34', '3,29', '3,67', '9,49']
    max_passenger = ['165', '170', '180', '190', '200', '210']


    with open ('planes.csv', 'w', newline='\n') as csvfile:
        writer = csv.writer(csvfile, delimiter = ';')
        writer.writerow(['Model', 'LoadCapacity', 'Efficinty', 'PassengerMax'])
        
        arr = ['0']*4
        
        for i in range(10):
            arr[0] = '\"' + plane_models[random.randint(0, len(plane_models) - 1)] + '\"'
            arr[1] = '\"' + load_capacity[random.randint(0, len(load_capacity) - 1)] + '\"'
            arr[2] = '\"' + efficiencies[random.randint(0, len(efficiencies) - 1)] + '\"'
            arr[3] = '\"' + max_passenger[random.randint(0, len(max_passenger) - 1)] + '\"'
            
            writer.writerow(arr)
        
def passenger():  
    fio = ['Zamotohina Maria Andreevna', 'Nenashev Ivan Nickolayevich', 
        'Chumackov Michail Vladimirovich', 'Zemlyanov Vladimir Borisovich', 'Belyackova Veronicka Sergeevna']
    passport = ['3678678903', '1234567890', '5678359284', '7492748492', '837968585', '2875909090']
    seats = ['A', 'B', 'C', 'D', 'E', 'F']
    flying_number = ["1000", "2000", "3000", "4000", "5000"]

    with open ('passengers.csv', 'w', newline='\n') as csvfile:
        writer = csv.writer(csvfile, delimiter = ';')
        writer.writerow(['FIO', 'Passport', 'Registration', 'FlyingNumber', 'SeatNumber', 'BaggageWeight'])
        arr = ['0']*6
        
        for i in range(15):
            arr[0] = '\"' + fio[random.randint(0, len(fio) - 1)] + '\"'
            arr[1] = '\"' + passport[random.randint(0, len(passport) - 1)] + '\"'
            arr[2] = "\"1\""
            arr[3] = '\"' + flying_number[random.randint(0, len(flying_number) - 1)] + '\"'
            arr[4] = '\"' + f"{seats[random.randint(0, len(seats) - 1)]}" + f"{random.randint(1, 20)}" + '\"'
            arr[5] = '\"' + f"{random.randint(10, 100)/10}" + '\"'
            
            writer.writerow(arr)
            
def airflying():  
    plane_models = ['NG Model 15001', 'NG Model 20103', 'Panda 202015', 'Panda 202208', 'NG Model 22009', 'Herpa 515726']
    flying_number = ["1000", "2000", "3000", "4000", "5000"]
    deparure_points = ['Tokio', 'Moscow', 'Samara', 'Rome', 'London', 'Los-Angeles']
    arrive_points = ['St. Petersburg', 'Washington', 'Dublin', 'Amsterdam', 'Vienna']
    

    with open ('airflyights.csv', 'w', newline='\n') as csvfile:
        writer = csv.writer(csvfile, delimiter = ';')
        writer.writerow(['CodeNumber', 'DeparturePoint', 'ArrivalPoint', 'Departure', 'Arrive', 'FlyingTime', 'PlaneType'])
        arr = ['0']*7
        
        for i in range(15):
            arr[0] = '\"' + flying_number[random.randint(0, len(flying_number) - 1)] + '\"'
            arr[1] = '\"' + deparure_points[random.randint(0, len(deparure_points) - 1)] + '\"'
            arr[2] = '\"' + arrive_points[random.randint(0, len(arrive_points) - 1)] + '\"'
            depar = random.randint(0, 24)
            arrive = random.randint(0, 24)
            while (depar >= arrive): 
                arrive = random.randint(0, 24)
            arr[3] = '\"' + "2024-09-19T" + f"{depar}:00:00" + '\"'
            arr[4] = '\"' + "2024-09-19T" + f"{arrive}:00:00" + '\"'
            arr[5] = '\"' + f"{arrive-depar}" + '\"'
            arr[6] = '\"' + plane_models[random.randint(0, len(plane_models) - 1)] + '\"'
            
            writer.writerow(arr)
planes()    
passenger()
airflying()