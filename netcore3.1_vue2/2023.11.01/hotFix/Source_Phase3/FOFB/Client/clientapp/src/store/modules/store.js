const state = {
  isCreateRouter: false
}

const mutations = {
  increment (state, payload) {
    return state.isCreateRouter = payload;
  }
}

const actions = {
  increment (context) {
    context.commit('increment')
  }
}

export default {
  state,
  mutations,
  actions
}