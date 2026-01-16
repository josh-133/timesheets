const BASE_URL = 'http://localhost:5102/api/timesheets'

export async function getTimesheets() {
    const res = await fetch(BASE_URL);
    return res.json();
}

export async function getTimesheet(id: number) {
    const res = await fetch(`${BASE_URL}/${id}`);
    return res.json();
}

export async function createTimesheet(data: any) {
    await fetch(BASE_URL,  {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
}

export async function updateTimesheet(id: number, data: any) {
    await fetch(`${BASE_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
}

export async function deleteTimesheet(id: number) {
    await fetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
    });
}