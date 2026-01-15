<script setup lang="ts">
  import { createTimesheet } from '../services/timesheetApi';
  import { ref } from 'vue'

  const entry = ref({
    date: null,
    hours: 0,
    description: "",
  })

  function onclick() {
    createTimesheet(entry.value)
  }
</script>

<template>
  <h2>Timesheet Form</h2>
  <form>
    <div>Date
      <input v-model="entry.date" type="date" placeholder="Enter the date"/>
    </div>
    <div>Hours
      <input v-model="entry.hours" type="text" placeholder="Enter no. hours"/>
    </div>
    <div>Description
      <input v-model="entry.description" type="text" placeholder="Describe what you've done"/>
    </div>
    <button @click="onclick">Submit</button>
  </form>
</template>

<style scoped>
</style>
