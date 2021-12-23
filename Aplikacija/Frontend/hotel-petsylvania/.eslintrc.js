module.exports = {
    extends: [
      // add more generic rulesets here, such as:
      // 'eslint:recommended',
      'plugin:vue/recommended'
    ],
    rules: {
      // override/add rules settings here, such as:
      // 'vue/no-unused-vars': 'error'
      // eslint-disable vue/require-prop-types //
      "vue/v-bind-style":0,
      "vue/singleline-html-element-content-newline": 0,
      'vue/max-attributes-per-line':0,
      "vue/html-closing-bracket-spacing":0,
      "vue/html-self-closing": 0,
      "vue/html-closing-bracket-newline":0,
      "vue/no-irregular-whitespace": 0,
      "vue/require-default-prop": 0,
      "vue/no-unused-components":0,
      "vue/attributes-order":0
    }
  }