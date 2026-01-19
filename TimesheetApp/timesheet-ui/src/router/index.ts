import { createRouter, createWebHistory } from 'vue-router'
import WeeklyTimesheetList from '../components/WeeklyTimesheetList.vue'
import TimesheetView from '../components/TimesheetView.vue'
import TimesheetForm from '../components/TimesheetForm.vue'

const routes = [
    {
        path: '/',
        component: WeeklyTimesheetList
    },
    {
        path: '/week/:id',
        component: TimesheetView
    },
    {
        path: '/create',
        component: TimesheetForm
    },
    {
        path: '/edit/:id',
        component: TimesheetForm
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router 