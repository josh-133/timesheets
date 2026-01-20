<script setup lang="ts">
    import { ref, onMounted } from 'vue';
    import { getWeeklyTimesheets, deleteWeeklyTimesheet } from '../services/timesheetApi';
    import type { WeeklyTimesheet } from '../types/timesheet';
    import { useRouter } from 'vue-router'
    import ConfirmModal from './ConfirmModal.vue'; 
    import { TrashIcon } from '@heroicons/vue/16/solid';
  
    const router = useRouter()
    const loading = ref(true)
    const timesheets = ref<WeeklyTimesheet[]>([])
    const error = ref('')
    const showModal = ref(false)
    const timesheetToDelete = ref<number | null>(null)

    onMounted(async () => {
        try {
            loading.value = true
            timesheets.value = await getWeeklyTimesheets()
        } catch (e) {
            error.value = 'Failed to load timesheets. Please try again.'
        } finally {
            loading.value = false
        }
    })

    function confirmDelete(id: number) {
        timesheetToDelete.value = id
        showModal.value = true
    }

    async function handleConfirmDelete() {
        if (timesheetToDelete.value !== null) {
            try {
                await deleteWeeklyTimesheet(timesheetToDelete.value)
                timesheets.value = timesheets.value.filter(e => e.id !== timesheetToDelete.value)
            } catch (e) {
                error.value = 'Failed to delete timesheet. Please try again.'
            }
        }
        showModal.value = false
        timesheetToDelete.value = null
    }

    function handleCancelDelete() {
        showModal.value = false
        timesheetToDelete.value = null
    }

    function viewWeeklyTimesheet(timesheet: WeeklyTimesheet) {
        router.push(`/week/${timesheet.id}`)
    }
</script>

<template>
    <div class="max-w-6xl mx-auto">
        <div class="flex justify-between items-center">
            <h2>Timesheets</h2>
            <button class="bg-green-500 p-2">
                <router-link to="/create">New Entry</router-link>
            </button>
        </div>
        <div v-if="error" class="bg-red-100 text-red-700 p-4 rounded">
            {{ error }}
        </div>
        <div v-if="loading" class="flex items-center justify-center gap-2 py-8">
            <div class="animate-spin h-8 w-8 border-4 border-blue-500 border-t-transparent rounded-full"></div>
            <span>Loading...</span>
        </div>
        <div v-else-if="timesheets.length > 0">
            <table class="w-full">
                <tr>
                    <th>Week Start Date</th>
                    <th>Options</th>
                </tr>
                <tr v-for="timesheet in timesheets" :key="timesheet.id" class="cursor-pointer hover:bg-gray-200" @click="viewWeeklyTimesheet(timesheet)">
                    <td>{{ timesheet.weekStartDate }}</td>
                    <td>
                        <button class="text-red-500 p-2 relative group" @click.stop="confirmDelete(timesheet.id)">
                            <TrashIcon class="h-5 w-5" />
                            <span class="tooltip">Delete</span>
                        </button>
                    </td>
                </tr>
            </table>
        </div>
        <div v-else>
            <p class="text-black">No timesheets yet, they are automatically created when you create a timesheet entry.</p>
            <router-link class="text-black" to="/create">Create your first entry</router-link>
        </div>
    </div>
        
        <ConfirmModal 
            :show="showModal"
            title="Delete Timesheet"
            message="Are you sure you want to delete this timesheet?"
            @confirm="handleConfirmDelete"
            @cancel="handleCancelDelete"
        />
  </template>
  
<style scoped>
.tooltip {
    transform: translateX(calc(-50% + 0.5rem)) translateY(0.5rem);
}
</style>