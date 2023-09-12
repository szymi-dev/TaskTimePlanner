import React, {useState, useEffect} from 'react';
import axios from 'axios';

function TaskRequest(){
    const url="https://localhost:44334/api/Task/CalculateNextExecution";
    const[data, setData] = useState({
      startDate: "",
      interval: "",
      dayOfWeek: ""
    });

    const [showDisplayButton, setShowDisplayButton] = useState(false);
    const[result, setResult] = useState(null);


    useEffect(() => {

      }, []);

    function handle(e){
      const newData={...data}
      newData[e.target.id] = e.target.value
      setData(newData)
      console.log(newData)
    }

    function fetchResults(e){
        axios.get('https://localhost:44334/api/Task/GetLastCalculationResult')
          .then(response => {
            setResult(response.data);
          })
          .catch(error => {
            console.error('Błąd podczas pobierania danych:', error);
          });
          
    }

    function sumbit(e){
        e.preventDefault();

        const today = new Date();
        const selectedDate = new Date(data.startDate);

        if (selectedDate > today) {
            console.error("Wybrana data nie może być większa niż dzisiejsza data.");
            return;
        }
        axios.post(url, {
            startDate: data.startDate,
            interval: parseInt(data.interval),
            dayOfWeek: parseInt(data.dayOfWeek)
        })
            .then(response => {
                console.log(response)
                setShowDisplayButton(true)
                fetchResults();
            })
            .catch(error => {
                console.error("Błąd Axios:", error);
            });
    }

    return(
        <div>
            <form onSubmit={(e) => sumbit(e)}>
                <div>
                    <label htmlFor="startDate">Wybierz datę z przeszłości:</label>
                    <input
                    onChange={(e) => handle(e)}
                    value={data.startDate}
                    id="startDate"
                    type="date"
                    />
                </div>
                <div>
                    <label htmlFor="interval">Liczba interwałów:</label>
                    <input
                    onChange={(e) => handle(e)}
                    value={data.interval}
                    id="interval"
                    type="number"
                    min="1"
                    />
                </div>
                <div>
                    <label htmlFor="dayOfWeek">Wybierz dzień tygodnia:</label>
                    <select
                    onChange={(e) => handle(e)}
                    value={data.dayOfWeek}
                    id="dayOfWeek"
                    >
                    <option value={0}>Niedziela</option>
                    <option value={1}>Poniedziałek</option>
                    <option value={2}>Wtorek</option>
                    <option value={3}>Środa</option>
                    <option value={4}>Czwartek</option>
                    <option value={5}>Piątek</option>
                    <option value={6}>Sobota</option>
                    </select>
                </div>
            <button>Oblicz</button>
            </form>
            {showDisplayButton && (
               <div>
               {result ? (
                 <div>
                   <h1>Ostatni wynik obliczenia:</h1>
                   <p>Data dzisiejsza: {result.today}</p>
                   <p>Pierwsze wystąpienie: {result.firstOccurrence}</p>
                   <p>Liczba wystąpień: {result.occurrenceCount}</p>
                   <p>Ostatnie wystąpienie: {result.lastOccurrence}</p>
                   <p>Następne wykonanie: {result.nextExecution}</p>
                 </div>
               ) : (
                 <p>Ładowanie danych...</p>
               )}
             </div>
            )}
        </div>
    )
}

export default TaskRequest;