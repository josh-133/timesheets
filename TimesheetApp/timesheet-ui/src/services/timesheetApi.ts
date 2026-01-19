
const BASE_URL = 'http://localhost:5102/api'

// --------------------------- ENTRIES API REQUESTS ---------------------------

export async function getTimesheetEntries() {
    const res = await fetch(BASE_URL + '/timesheetentries');
    if (!res.ok) {
        throw new Error('Failed to fetch timesheet entries')
    }
    return res.json();
}

export async function getTimesheetEntry(id: number) {
    const res = await fetch(`${BASE_URL}/timesheetentries/${id}`);
    if (!res.ok) {
        throw new Error('Failed to fetch timesheet entry')
    }
    return res.json();
}

export async function createTimesheetEntry(data: any) {
    const res = await fetch(BASE_URL + '/timesheetentries',  {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (!res.ok) {
        throw new Error('Failed to create timesheet entry')
    }
}

export async function updateTimesheetEntry(id: number, data: any) {
    const res = await fetch(`${BASE_URL}/timesheetentries/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (!res.ok) {
        throw new Error('Failed to update timesheet entry')
    }
}

export async function deleteTimesheetEntry(id: number) {
    const res = await fetch(`${BASE_URL}/timesheetentries/${id}`, {
        method: 'DELETE',
    });
    if (!res.ok) {
        throw new Error('Failed to delete timesheet entry')
    }
}

// --------------------------- WEEKLY TIMESHEETS API REQUESTS ---------------------------

export async function getWeeklyTimesheets() {
    const res = await fetch(BASE_URL + '/weeklytimesheets');
    if (!res.ok) {
        throw new Error('Failed to fetch weekly timesheets')
    }
    return res.json();
}

export async function getWeeklyTimesheet(id: number) {
    const res = await fetch(`${BASE_URL}/weeklytimesheets/${id}`);
    if (!res.ok) {
        throw new Error('Failed to fetch weekly timesheet')
    }
    return res.json();
}

export async function createWeeklyTimesheet(data: any) {
    const res = await fetch(BASE_URL + '/weeklytimesheets',  {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (!res.ok) {
        throw new Error('Failed to create weekly timesheet')
    }
}

export async function deleteWeeklyTimesheet(id: number) {
    const res = await fetch(`${BASE_URL}/weeklytimesheets/${id}`, {
        method: 'DELETE',
    });
    if (!res.ok) {
        throw new Error('Failed to delete weekly timesheet')
    }
}