<script setup lang="ts">
  import { getTimesheet, createTimesheet, updateTimesheet } from '../services/timesheetApi'
  import { ref, watch } from 'vue'
  import { onMounted } from 'vue'
  import type { TimesheetEntry } from '../types/timesheet'
  import { useRoute, useRouter } from 'vue-router'

  const route = useRoute();
  const router = useRouter();
  const isCreate = !route.params.id;
  const entry = ref ({} as TimesheetEntry)

  onMounted(async () => {
    if (route.params.id) {
      entry.value = await getTimesheet(Number(route.params.id));
    }
  })


  async function submit() {
    if (isCreate) { 
      await createTimesheet(entry.value)
    } else {
      await updateTimesheet(entry.value.id, entry.value)
    }
    router.push('/')
  }
</script>

<template>
  <h2>Timesheet Form</h2>
  <form class="bg-gray-100 p-12 rounded-md shadow-md space-y-4">
    <div>
      <label>Date</label>
      <input v-model="entry.date" type="date" placeholder="Enter the date"/>
    </div>
    <div>
      <label>Hours</label>
      <input v-model="entry.hours" type="text" placeholder="Enter no. hours"/>
    </div>
    <div>
      <label>Description</label>
      <input v-model="entry.description" type="text" placeholder="Describe what you've done"/>
    </div>
    <button type="button" class="bg-black text-white m-1" @click.prevent="submit">Submit</button>
  </form>
</template>

<style scoped>
</style>
