<script setup lang="ts">
  import { getTimesheetEntry, createTimesheetEntry, updateTimesheetEntry } from '../services/timesheetApi'
  import { ref } from 'vue'
  import { onMounted } from 'vue'
  import type { TimesheetEntry } from '../types/timesheet'
  import { useRoute, useRouter } from 'vue-router'
  import { computed } from 'vue'

  const route = useRoute()
  const router = useRouter()
  const loading = ref(false)
  const saving = ref(false)
  const error = ref('')
  const isCreate = !route.params.id
  const today = new Date().toISOString().split('T')[0]
  const entry = ref ({
    date: today,
    startTime: '09:00',
    endTime: '17:00',
    breakTime: '0',
    description: '',
  } as TimesheetEntry)

  const calculatedHours = computed(() => {
    const start = entry.value.startTime
    const end = entry.value.endTime
    const breakMins = Number(entry.value.breakTime) || 0

    if (!start || !end) return 0

    const [startH, startM] = start.split(':').map(Number)
    const [endH, endM] = end.split(':').map(Number)

    const startMinutes = startH * 60 + startM
    const endMInutes = endH * 60 + endM

    const workedMinutes = endMInutes - startMinutes - breakMins

    return Math.round((workedMinutes / 60) * 10) / 10

    
  })

  const errors = ref({
    date: '',
    startTime: '',
    endTime: '',
    breakTime: '',
    description: '',
  })

  function validate(): boolean {
    let isValid = true

    errors.value = { date: '', startTime: '', endTime: '', breakTime: '', description: '' }
    if ( !entry.value.date ) {
      errors.value.date = "Date is required"
      isValid = false
    }
    if ( entry.value.startTime > entry.value.endTime ) {
      errors.value.startTime = "Start time must be earlier than end time"
      isValid = false
    }
    if (!Number(entry.value.breakTime)) {
      errors.value.breakTime = "Invalid break time entry"
      isValid = false
    }
    if ( !entry.value.description ) {
      errors.value.description = "Description must not be empty"
      isValid = false
    }

    return isValid
  }

  onMounted(async () => {
    if (route.params.id) {
      try {
        loading.value = true
        entry.value = await getTimesheetEntry(Number(route.params.id))
      } catch (e) {
        error.value = 'Failed to get entry. Please try again.'
      } finally { 
        loading.value = false
      }
    }
  })


  async function submit() {
    if (validate()) {
      saving.value = true
      try {
        if (isCreate) {
          await createTimesheetEntry(entry.value)
        } else {
          await updateTimesheetEntry(entry.value.id, entry.value)
        }
        router.push('/')
      } catch (e) {
          error.value = 'Failed to save entry. Please try again.'
      } finally {
        saving.value = false
      }
    }
  }
</script>

<template>
  <h2>Timesheet Form</h2>
  <div v-if="error" class="bg-red-100 text-red-700 p-4 rounded">
    {{ error }}
  </div>
  <div v-if="loading" class="flex items-center justify-center gap-2 py-8">
    <div class="animate-spin h-8 w-8 border-4 border-blue-500 border-t-transparent rounded-full"></div>
    <span>Loading...</span>
  </div>
  <form v-else class="bg-gray-100 p-12 rounded-md shadow-md space-y-4">
    <div>
      <label>Date</label>
      <input v-model="entry.date" @input="errors.date=''" type="date" placeholder="Enter the date"/>
      <span class="text-red-500 text-sm">{{ errors.date }}</span>
    </div>
    <div>
      <label>Start Time</label>
      <input v-model="entry.startTime" @input="errors.startTime=''" type="time"/>
      <span class="text-red-500 text-sm">{{ errors.startTime }}</span>
    </div>
    <div>
      <label>End Time</label>
      <input v-model="entry.endTime" @input="errors.endTime=''" type="time"/>
      <span class="text-red-500 text-sm">{{ errors.endTime }}</span>
    </div>
    <div>
      <label>Break</label>
      <input v-model="entry.breakTime" @input="errors.breakTime=''" type="text"/>
      <span class="text-red-500 text-sm">{{ errors.breakTime }}</span>
    </div>
    <div class="text-black">
      <label>Hours</label>
      {{ calculatedHours }}
    </div>
    <div>
      <label>Description</label>
      <textarea v-model="entry.description" @input="errors.description=''" rows="3" placeholder="Describe what you've done"></textarea>
      <span class="text-red-500 text-sm">{{ errors.description }}</span>
    </div>
    <button type="button" class="bg-black text-white m-1 p-2" :disabled="saving" @click.prevent="submit">{{ saving ? 'Saving...' : 'Submit' }}</button>
  </form>
</template>

<style scoped>
</style>
